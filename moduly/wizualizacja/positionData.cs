namespace HAL062app.moduly.wizualizacja
{
    public class positionData
    {
        private float[] _angles = new float[6];
        public positionData(float[] angles)
        {
            _angles = angles;


        }
        public float[] GetAngles => _angles;
        public void SetAngles(float[] angles)
        {
            _angles = angles;
        }
    }


    public class specialPosition : positionData
    {
        private string _ID;
        public specialPosition(string ID, float[] angles) : base(angles)
        {
            _ID = ID;
        }

    }


}
