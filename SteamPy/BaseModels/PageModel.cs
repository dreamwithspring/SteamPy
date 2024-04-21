namespace steamPy.BaseModels
{
    public class PageModel<T> where T : class
    {
        public PageModel()
        {
            Result = new List<T>();
        }

        public List<T> Result { get; set; }

        public int Total { get; set; }

        public int Size { get; set; }

        public int Current { get; set; }
    }
}
