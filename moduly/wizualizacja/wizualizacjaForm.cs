using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace HAL062app.moduly.wizualizacja
{


 

    public partial class wizualizacjaForm : Form
    {
        internal class ViewportManager
        {
            private List<HelixViewport3D> _viewports;
            private HelixViewport3D _mainViewport;

            public ViewportManager(HelixViewport3D mainViewport)
            {
                _viewports = new List<HelixViewport3D>();
                _mainViewport = mainViewport;
            }

            public void AddViewport(HelixViewport3D viewport)
            {
                _viewports.Add(viewport);
                SyncViewports(viewport);
            }

            public void RemoveViewport(HelixViewport3D viewport)
            {
                _viewports.Remove(viewport);
            }

            public List<HelixViewport3D> GetViewports()
            {
                return _viewports;
            }

            private void SyncViewports(HelixViewport3D viewport)
            {
                // Synchronizacja transformacji i innych zmian
                _mainViewport.Camera.Changed += (s, e) => viewport.Camera = _mainViewport.Camera;
                // Dodaj inne synchronizacje w razie potrzeby
            }
        }


        private ViewportManager _viewportManager;
        private HelixViewport3D _mainViewport;
        public wizualizacjaForm()
        {
            InitializeComponent();
            InitializeMainViewport();
            _viewportManager = new ViewportManager(_mainViewport);
            
        }


        private void InitializeMainViewport()
        {
            _mainViewport = new HelixViewport3D();
            var elementHost = new ElementHost
            {
                Dock = DockStyle.Fill,
                Child = _mainViewport
            };
            Controls.Add(elementHost);
        }

        
        private void AddNewViewportWindow()
        {
            var newViewport = new HelixViewport3D();
            _viewportManager.AddViewport(newViewport);

            var form = new Form
            {
                Text = "Additional Viewport",
                Width = 800,
                Height = 600
            };

            var elementHost = new ElementHost
            {
                Dock = DockStyle.Fill,
                Child = newViewport
            };

            form.Controls.Add(elementHost);
            form.FormClosed += (s, e) => _viewportManager.RemoveViewport(newViewport);
            form.Show();
        }
    }
}
