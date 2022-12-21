module day19_try3.Cache

type Cache() =
    override this.ToString() = $"Cache()"
    
    static member empty = Cache() 