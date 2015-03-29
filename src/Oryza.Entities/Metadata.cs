using System;

namespace Oryza.Entities
{
    public struct Metadata
    {
        public Metadata(DateTime createdAt, DateTime lastModifiedAt)
        {
            this.createdAt = createdAt;
            this.lastModifiedAt = lastModifiedAt;
        }

        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        public DateTime LastModifiedAt
        {
            get { return lastModifiedAt; }
            set { lastModifiedAt = value; }
        }

        private DateTime createdAt;
        private DateTime lastModifiedAt;
    }
}