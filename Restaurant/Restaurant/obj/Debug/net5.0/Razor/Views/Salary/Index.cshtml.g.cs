#pragma checksum "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5947662c19728f59080eeb1b913fedcddb07a811"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Salary_Index), @"mvc.1.0.view", @"/Views/Salary/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5947662c19728f59080eeb1b913fedcddb07a811", @"/Views/Salary/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3252bb80a0572243a0a2c06d4a14dfe742524a68", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Salary_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Salary>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<hr class=""my-5"" />

<!-- Bordered Table -->
<div class=""card"">
    <div class=""card-header"" style=""display:flex;justify-content:space-between"">
        <h2>Maaşlar</h2>    
    </div>
    <div class=""card-body"">
        <div class=""table-responsive text-nowrap"">
            <table id=""table"" class=""table table-bordered"">
                <thead>
                    <tr>
                        <th>Ad</th>
                        <th>Tarix</th>
                        <th>Miqdar</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 21 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Index.cshtml"
                     foreach (Salary item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 24 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Index.cshtml"
                           Write(item.Employee);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 25 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Index.cshtml"
                           Write(item.CreatedTime.ToString("dd MMMM, yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 26 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Index.cshtml"
                           Write(item.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 28 "C:\Users\Esma\Desktop\GitHub Project\Restaurant-AdminPanel\Restaurant\Restaurant\Views\Salary\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/ Bordered Table -->\r\n\r\n<hr class=\"my-5\" />\r\n\r\n\r\n\r\n");
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
