using E_Ticaret.Models;
using System.Text.Json;

namespace E_Ticaret.Extensions
{
    public static class SessionExtensions
    {
        public static Cart GetCart(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? new Cart() : JsonSerializer.Deserialize<Cart>(value);
        }

        public static void SetCart(this ISession session, string key, Cart cart)
        {
            session.SetString(key, JsonSerializer.Serialize(cart));
        }
    }
}
