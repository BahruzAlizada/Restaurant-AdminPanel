#pragma checksum "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e5aac7ba787df7559422677fad695e70297f7b6a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Salary_Detail), @"mvc.1.0.view", @"/Views/Salary/Detail.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\_ViewImports.cshtml"
using Restaurant;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\_ViewImports.cshtml"
using Restaurant.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\_ViewImports.cshtml"
using Restaurant.ViewsModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5aac7ba787df7559422677fad695e70297f7b6a", @"/Views/Salary/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3252bb80a0572243a0a2c06d4a14dfe742524a68", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Salary_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Salary>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Detail.cshtml"
  
    int count = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""card-body"">
    <div class=""table-responsive text-nowrap"">
        <table id=""table"" class=""table table-bordered"">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Ad</th>
                    <th>Tarix</th>
                    <th>Miqdar</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 17 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Detail.cshtml"
                 foreach (Salary item in Model)
                {
                    count++;

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 21 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Detail.cshtml"
                       Write(count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 22 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Detail.cshtml"
                       Write(item.Employee);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 23 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Detail.cshtml"
                       Write(item.CreatedTime.ToString("dd MMMM, yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 24 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Detail.cshtml"
                       Write(item.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>         \r\n");
#nullable restore
#line 26 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Detail.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Salary>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
