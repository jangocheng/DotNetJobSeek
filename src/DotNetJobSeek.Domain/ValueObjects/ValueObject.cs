namespace DotNetJobSeek.Domain
{
    public abstract class ValueObject
    {
        public int Id { set; get; }
        public string Name { set; get; }   
    }
}