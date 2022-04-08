using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }
    public IActionResult Index()
    {
        //read cookie from IHttpContextAccessor  
        string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["key"];
        //read cookie from Request object  
        string cookieValueFromReq = Request.Cookies["Key"];
        //set the key value in Cookie  
        Set("kay", "Hello from cookie", 10);
        //Delete the cookie object  
        Remove("Key");
        return View();
    }
    /// <summary>  
    /// Get the cookie  
    /// </summary>  
    /// <param name="key">Key </param>  
    /// <returns>string value</returns>  
    public string Get(string key)
    {
        return Request.Cookies[key];
    }
    /// <summary>  
    /// set the cookie  
    /// </summary>  
    /// <param name="key">key (unique indentifier)</param>  
    /// <param name="value">value to store in cookie object</param>  
    /// <param name="expireTime">expiration time</param>  
    public void Set(string key, string value, int? expireTime)
    {
        Console.WriteLine(key, value);
        CookieOptions option = new CookieOptions();
        option.Domain = "localhost";
        option.HttpOnly = true;
        option.SameSite = SameSiteMode.None;
        option.MaxAge = System.TimeSpan.FromDays(1);
        option.Secure = true;

        if (expireTime.HasValue)
        {
            option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        }
        else
        {
            option.Expires = DateTime.Now.AddMilliseconds(10);
        }
        Response.Cookies.Append(key, value, option);
    }
    /// <summary>  
    /// Delete the key  
    /// </summary>  
    /// <param name="key">Key</param>  
    public void Remove(string key)
    {
        Response.Cookies.Delete(key);
    }
}