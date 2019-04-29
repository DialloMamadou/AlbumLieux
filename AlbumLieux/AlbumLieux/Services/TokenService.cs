using AlbumLieux.Models;
using MonkeyCache.SQLite;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlbumLieux.Services
{
    public interface ITokenService
    {
        void AddToken(Token tokens);
        Token GetToken();
        bool HasToken();
        void Disconnect();
        Task Refresh();
    }

    public class TokenService : ITokenService
    {
        private const string TOKENS = "TOKENS";

        private readonly Lazy<IUserDataService> _userDataService;

        public TokenService()
        {
            _userDataService = new Lazy<IUserDataService>(() => DependencyService.Resolve<IUserDataService>());
        }

        public void AddToken(Token tokens)
        {
            var timeSpanExpiresIn = TimeSpan.FromSeconds(tokens.ExpiresIn);
            tokens.ExpiresAt = DateTime.Now + timeSpanExpiresIn;
            Barrel.Current.Add(TOKENS, tokens, timeSpanExpiresIn);
        }

        public void Disconnect()
        {
            Barrel.Current.Empty(TOKENS);
        }

        public Token GetToken()
        {
            return Barrel.Current.Get<Token>(TOKENS);
        }

        public bool HasToken()
        {
            return Barrel.Current.Exists(TOKENS);
        }

        public async Task Refresh()
        {
            if (Barrel.Current.Exists(TOKENS))
            {
                var refreshToken = Barrel.Current.Get<Token>(TOKENS).RefreshToken;
                await _userDataService.Value.Refresh(refreshToken);
            }
        }
    }
}
