using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Models;
using UI;

namespace test1.Pages
{
    public class IndexModel : PageModel
    {
        public IOptions<SocketInfo> SocketInfo { get; set; }
        public IndexModel(IOptions<SocketInfo> socketInfo)
        {
            SocketInfo = socketInfo;
        }
        public void OnGet()
        {
            ViewData["socketUrl"] = SocketInfo.Value.SocketUrl;
        }
    }
}
