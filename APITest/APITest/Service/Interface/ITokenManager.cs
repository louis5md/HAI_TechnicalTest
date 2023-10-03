namespace APITest.Service.Interface
{
    public interface ITokenManager
    {
        public Task<bool> isCurrentTokenInDeactivatedList();
        public Task DeactivateCurrentToken();
        public Task<bool> isInDeactivatedListToken(string token);
        public Task DeactivateToken(string token);
    }
}
