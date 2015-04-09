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

        public static bool operator ==(Metadata self, Metadata other)
        {
            return self._createdAt == other._createdAt &&
                   self._lastModifiedAt == other._lastModifiedAt;
        }

        public static bool operator !=(Metadata self, Metadata other)
        {
            return !(self == other);
        }

        private DateTime _createdAt;
        private DateTime _lastModifiedAt;
    }
}