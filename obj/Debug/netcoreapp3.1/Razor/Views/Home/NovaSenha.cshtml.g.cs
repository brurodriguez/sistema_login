#pragma checksum "C:\Users\bruno\source\repos\SistemaLogin\Views\Home\NovaSenha.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9f4b561a6f3f28536b3b5e17cafa9e06a25bb772"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_NovaSenha), @"mvc.1.0.view", @"/Views/Home/NovaSenha.cshtml")]
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
#line 1 "C:\Users\bruno\source\repos\SistemaLogin\Views\_ViewImports.cshtml"
using SistemaLogin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\bruno\source\repos\SistemaLogin\Views\_ViewImports.cshtml"
using SistemaLogin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9f4b561a6f3f28536b3b5e17cafa9e06a25bb772", @"/Views/Home/NovaSenha.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9c0e9a059d226086b586f8ad228b41b9ed2bb1a8", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_NovaSenha : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\bruno\source\repos\SistemaLogin\Views\Home\NovaSenha.cshtml"
  
    ViewData["Title"] = "Redefinir Senha";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"divLayout\">\r\n    <div class=\"text-center\">\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9f4b561a6f3f28536b3b5e17cafa9e06a25bb7723760", async() => {
                WriteLiteral("\r\n            <h1 class=\"display-4\">Redefinir Senha</h1>\r\n            <br />\r\n");
#nullable restore
#line 10 "C:\Users\bruno\source\repos\SistemaLogin\Views\Home\NovaSenha.cshtml"
              
                var PaginaTela = @TempData["PaginaTela"];
                if (PaginaTela is null)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                    <p>Insira seu e-mail para redefinir sua senha.</p>
                    <br />
                    <center>
                        <h3 class=""liter"">E-mail:</h3>
                    </center>
                    <input required type=""email"" id=""emailRedefinir"" name=""emailRedefinir"" class=""iptform"" />
                    <br /><br />
                    <center><button id=""btnRefeninir"" name=""btnRefeninir"" value=""1"" type=""submit"" class=""btn-toggle""><span class=""txtb"">Enviar</span></button></center>
");
#nullable restore
#line 22 "C:\Users\bruno\source\repos\SistemaLogin\Views\Home\NovaSenha.cshtml"

                }
                else if (PaginaTela.ToString() == "1")
                {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                    <p>Um código foi enviado para o seu e-mail! Por favor, digite o código e sua nova senha.</p>
                    <br />
                    <center>
                        <h3 class=""liter"">Código:</h3>
                    </center>
                    <input");
                BeginWriteAttribute("value", " value=\"", 1262, "\"", 1298, 1);
#nullable restore
#line 31 "C:\Users\bruno\source\repos\SistemaLogin\Views\Home\NovaSenha.cshtml"
WriteAttributeValue("", 1270, TempData["codigoNovaSenha"], 1270, 28, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" id=""codigoNovaSenha"" name=""codigoNovaSenha"" required class=""iptform"" type=""text"" />
                    <br /><br />
                    <center>
                        <h3 class=""liter"">Senha:</h3>
                    </center>
                    <input name=""senhaNova"" id=""senhaNova"" onkeyup=""javascript:verificaSenha('senhaNova','mostraNova')"" required class=""iptform"" type=""password"" />
                    <br /><center><table id=""mostraNova""></table></center><br />
                    <center>
                        <h3 class=""liter"">Digite sua nova senha novamente:</h3>
                    </center>
                    <input id=""senhaNovaRepetida"" name=""senhaNovaRepetida"" required class=""iptform"" type=""password"" />
                    <br /><br />
                    <center><button id=""btnRefeninir"" name=""btnRefeninir"" value=""2"" type=""submit"" class=""btn-toggle""><span class=""txtb"">Redefinir</span></button></center>
");
#nullable restore
#line 44 "C:\Users\bruno\source\repos\SistemaLogin\Views\Home\NovaSenha.cshtml"
                }
            

#line default
#line hidden
#nullable disable
                WriteLiteral("            <br />\r\n            <b style=\"color:#780000;\">");
#nullable restore
#line 47 "C:\Users\bruno\source\repos\SistemaLogin\Views\Home\NovaSenha.cshtml"
                                 Write(TempData["mensagemRedefinirSenha"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</b>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n\r\n</div>\r\n   ");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
