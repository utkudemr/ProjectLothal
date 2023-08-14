
namespace Core.Persistance.Dynamic
{
    public class Filter
    {
        public string Field { get; set; }
        public string? Value { get; set; }
        public string Operator { get; set; }
        public string? Logic { get; set; }
        public IEnumerable<Filter> Filters { get; set; }
        public Filter()
        {
            Filters= new List<Filter>();
            Operator = string.Empty;
        }
        public Filter(string field, string @operator)
        {
            Operator = @operator;
        }
    }
}
