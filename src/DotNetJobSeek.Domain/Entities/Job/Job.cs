using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetJobSeek.Domain
{
    public class Job : Entity, IAggregateRoot, IPublisher
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Salary { get; set; }
        public string Email { get; set; }
        public string Contacts { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<Requirement> Requirements { get; set; }
        public virtual ICollection<JobTag> JobTags { get; set; }

        public string LocationGeohash { get; set; }
        public Location Location { get; set; }

    }
}