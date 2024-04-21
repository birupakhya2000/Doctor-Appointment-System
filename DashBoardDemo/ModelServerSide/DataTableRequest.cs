namespace DashBoardDemo.ModelServerSide
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public SearchFilter Search { get; set; }
        public List<Ordering> Order { get; set; }
        public List<Column> Columns { get; set; }

        
    }

    public class SearchFilter
    {
        public string Value { get; set; }
    }

    public class Ordering
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class Column
    {
        public string Data { get; set; }
    }
}
