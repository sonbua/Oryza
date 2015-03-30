using System;

namespace Oryza.Entities
{
    public struct Metadata
    {
        public Metadata(DateTime createdAt, DateTime lastModifiedAt)
        {
            _createdAt = createdAt;
            _lastModifiedAt = lastModifiedAt;
        }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        public DateTime LastModifiedAt
        {
            get { return _lastModifiedAt; }
            set { _lastModifiedAt = value; }
        }

        private DateTime _createdAt;
        private DateTime _lastModifiedAt;
    }
}