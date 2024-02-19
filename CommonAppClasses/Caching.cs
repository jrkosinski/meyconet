using System;
using System.Collections.Specialized;

namespace CommonAppClasses
{
    public class ObjectCache
    {
        private System.DateTime timeCached = DateTime.MinValue;

        public virtual object CachedObject { get; private set; }

        public virtual int SecondsExpiration { get; private set; }

        public virtual bool IsEnabled
        {
            get { return this.SecondsExpiration > 0; }
        }

        public ObjectCache(int secondsExpiration = 120)
        {
            this.SecondsExpiration = secondsExpiration;
        }

        public virtual int AgeSeconds
        {
            get { return (int)(System.DateTime.Now - timeCached).TotalSeconds; }
        }

        public virtual bool IsInvalid
        {
            get { return this.CachedObject == null || this.AgeSeconds > this.SecondsExpiration; }
        }

        public virtual void Refresh(object cachedObject)
        {
            if (this.IsEnabled)
            {
                this.CachedObject = cachedObject;
                this.timeCached = DateTime.Now;
            }
        }

        public virtual void Invalidate()
        {
            this.CachedObject = null;
        }
    }

    public class ObjectCacheWithParams : ObjectCache
    {
        public StringDictionary SearchParams { get; private set; }

        public ObjectCacheWithParams(int secondsExpiration = 120) : base(secondsExpiration)
        {
            this.SearchParams = new StringDictionary();
        }
    }
}
