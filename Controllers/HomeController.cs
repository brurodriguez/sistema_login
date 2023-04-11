using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaLogin.Models;

namespace SistemaLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult NovaSenha()
        {
            return View();
        }

        public IActionResult SistemaLogin()
        {
            try
            {
                /*VALIDAÇÃO DOS COOKIES*/
                var dataAtual = DateTime.Now;
                var horaAtual = dataAtual.ToString("HH");
                var MinutoAtual = dataAtual.ToString("mm");
                var key = "chave criptografada";
                var cookieCifrado = Criptografia.EncryptString(key, "verificado");
                var cookieValue = Request.Cookies[cookieCifrado];
                var cookieValorDecifrado = Criptografia.DecryptString(key, cookieValue);

                var cookieDataCifrado = Criptografia.EncryptString(key, "dataCookie");
                var cookieDataValue = Request.Cookies[cookieDataCifrado];
                var cookieValorDataDecifrado = Criptografia.DecryptString(key, cookieDataValue);
                var CookieHora = Convert.ToDateTime(cookieValorDataDecifrado).ToString("HH");
                var CookieMinutos = Convert.ToDateTime(cookieValorDataDecifrado).ToString("mm");

                var dataDiferenca = dataAtual - Convert.ToDateTime(cookieValorDataDecifrado);
                var dataDiferencaMinuto = dataDiferenca.ToString("mm");

                if (cookieValorDecifrado == "1" && horaAtual == CookieHora && Int32.Parse(dataDiferencaMinuto) < 10)
                {
                    /*CAPTURA DOS COOKIES*/

                    var cookieCifradoNmUsuario = Criptografia.EncryptString(key, "NM_USUARIO");
                    //cifra do cookie
                    var cookieValueNmUsuario = Request.Cookies[cookieCifradoNmUsuario];
                    //descript do cookie
                    var cookieValorDecifradoNmUsuario = Criptografia.DecryptString(key, cookieValueNmUsuario);

                    TempData["NomeUsuario"] = cookieValorDecifradoNmUsuario.ToString();
                    TempData["MenuAutomatico"] = "1";

                    return View();
                }
                else
                {
                    TempData["mensagemLogin"] = "Sua sessão foi encerrada. Por favor, realize seu login novamente.";
                    return Redirect("/Home");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["mensagemLogin"] = "Sua sessão foi encerrada. Por favor, realize seu login novamente.";
                return Redirect("/Home");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    // Get Post Valores
                    string nomeCompleto = collection["nomeCompleto"];
                    string email = collection["email"]; 
                    string senha = collection["senha"];
                    string senhaRepetida = collection["senhaRepetida"];

                    /*Mantem os dados registrados*/
                    TempData["nomeCompleto"] = nomeCompleto;
                    TempData["email"] = email;

                    /*Validação da confirmação de senha*/

                    if (senha == senhaRepetida)
                    {
                        /*********************************SELECT VALIDAÇÃO NO BANCO DE DADOS*********************************************/

                        StringBuilder ConsultaCadastro = new StringBuilder();
                        ConsultaCadastro.Append("SELECT COUNT(*) FROM TBL_SISTEMA_LOGIN WITH(NOLOCK)");
                        ConsultaCadastro.Append(string.Format(" WHERE EMAIL LIKE '%{0}%'", email));

                        DataBase selectConsultaCadastro = new DataBase();
                        dynamic CadastroExistente = selectConsultaCadastro.AcaoDataBase(ConsultaCadastro.ToString(), 1, 1, "SELECT");

                        /*Verifica Usuário Existente*/

                        if (CadastroExistente[0].ToString() == "0")
                        {
                            /*********************************CRIPTOGRAFIA EM MD5*********************************************/

                            var hashSenha = Criptografia.CreateMD5(senha.ToString());

                            /*********************************INSERÇÃO NO BANCO DE DADOS*********************************************/

                            StringBuilder insertCodigo = new StringBuilder();
                            insertCodigo.Append("INSERT INTO TBL_SISTEMA_LOGIN( NM_COMPLETO, EMAIL, SENHA, DT_REGISTRO)");
                            insertCodigo.Append(string.Format(" VALUES('{0}', '{1}', '{2}', '{3}')", nomeCompleto, email, hashSenha.ToString(), DateTime.Now));
                            DataBase InsertBanco = new DataBase();
                            InsertBanco.AcaoDataBase(insertCodigo.ToString(), 0, 1, "INSERT");

                            /*********************************ENVIO DE EMAIL*********************************************/

                            EnvioEmail enviar = new EnvioEmail();
                            enviar.EnviarEmail(nomeCompleto, email, "Seu cadastro foi realizado com sucesso!");

                            TempData["mensagemConfirmacao"] = "Seu cadastro foi realizado com sucesso!";
                            TempData["nomeCompleto"] = "";
                            TempData["email"] = "";

                            return Redirect("/Home");
                        }
                        else
                        {
                            TempData["mensagem"] = "Usuário já cadastrado. Caso esqueceu a senha, você pode redefini-la no menu acima!";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["mensagem"] = "Sua confirmação de senha está incorreta! Por favor, insira a senha igualmente nos dois campos.";
                        return View();
                    }

                }
                else
                {
                    TempData["mensagem"] = "Ocorreu um erro inesperado, por favor tente novamente mais tarde, ou entre em contato conosco.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["mensagem"] = "Ocorreu um erro inesperado, por favor tente novamente mais tarde, ou entre em contato conosco.";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Get Post Valores
                    string emailLogin = collection["emailLogin"];
                    string senhaLogin = collection["senhaLogin"];

                    /*********************************SELECT VALIDAÇÃO NO BANCO DE DADOS*********************************************/

                    StringBuilder ConsultaCadastro = new StringBuilder();
                    ConsultaCadastro.Append("SELECT * FROM TBL_SISTEMA_LOGIN WITH(NOLOCK)");
                    ConsultaCadastro.Append(string.Format(" WHERE EMAIL LIKE '%{0}%'", emailLogin));

                    DataBase selectConsultaCadastro = new DataBase();
                    dynamic CadastroExistente = selectConsultaCadastro.AcaoDataBase(ConsultaCadastro.ToString(), 4, 1, "SELECT");

                    /*Verifica Usuário*/

                    if (CadastroExistente.Count == 0)
                    {
                        TempData["mensagemLogin"] = "Usuário incorreto! Esse usuário não existe.";
                        return View();
                    }
                    else
                    {
                        /*********************************CRIPTOGRAFIA EM MD5*********************************************/

                        var hashSenhaLogin = Criptografia.CreateMD5(senhaLogin.ToString());

                        /*Verifica Senha*/

                        if (hashSenhaLogin.ToString() == CadastroExistente[3].ToString())
                        {

                            /*********************************INSERÇÃO NO BANCO DE DADOS*********************************************/

                            StringBuilder insertCodigo = new StringBuilder();
                            insertCodigo.Append("INSERT INTO TBL_SISTEMA_LOGIN_LOG( ID_LOGIN, DT_LOGIN)");
                            insertCodigo.Append(string.Format(" VALUES('{0}', '{1}')", CadastroExistente[0].ToString(), DateTime.Now));
                            DataBase InsertBanco = new DataBase();
                            InsertBanco.AcaoDataBase(insertCodigo.ToString(), 0, 1, "INSERT");

                            /*********************************COOKIES*********************************************/

                            /*CONFIGUAÇÃO DE TEMPO LIMITE DOS COOKIES*/
                            var cookieOptions = new CookieOptions
                            {
                                Secure = false,
                                Expires = DateTime.Now.AddDays(1)
                            };

                            /*VALIDADE PARA VERIFICAÇÃO DOS COOKIES*/
                            var key = "chave criptografada";
                            var cookieCifrado = Criptografia.EncryptString(key, "verificado");
                            var valorCookie = Criptografia.EncryptString(key, "1");

                            var cookieDataCifrado = Criptografia.EncryptString(key, "dataCookie");
                            var valorCookieDataCifrado = Criptografia.EncryptString(key, DateTime.Now.ToString());

                            Response.Cookies.Append(cookieCifrado, valorCookie, cookieOptions);
                            Response.Cookies.Append(cookieDataCifrado, valorCookieDataCifrado, cookieOptions);

                            /*COOKIES DADOS USUARIO*/
                            var cookieCifradoIdUsuario = Criptografia.EncryptString(key, "ID_USUARIO");
                            var valorCookieCifradoIdUsuario = Criptografia.EncryptString(key, CadastroExistente[0].ToString());
                            Response.Cookies.Append(cookieCifradoIdUsuario, valorCookieCifradoIdUsuario, cookieOptions);

                            var cookieCifradoNmUsuario = Criptografia.EncryptString(key, "NM_USUARIO");
                            var valorCookieCifradoNmUsuario = Criptografia.EncryptString(key, CadastroExistente[1].ToString());
                            Response.Cookies.Append(cookieCifradoNmUsuario, valorCookieCifradoNmUsuario, cookieOptions);

                            var cookieCifradoSobreEmailUsuario = Criptografia.EncryptString(key, "EMAIL_USUARIO");
                            var valorCookieCifradoEmailUsuario = Criptografia.EncryptString(key, CadastroExistente[2].ToString());
                            Response.Cookies.Append(cookieCifradoSobreEmailUsuario, valorCookieCifradoEmailUsuario, cookieOptions);

                            return Redirect("/Home/SistemaLogin");
                        }
                        else
                        {
                            TempData["mensagemLogin"] = "Senha incorreta!";
                            return View();
                        }

                    }

                }
                else
                {
                    TempData["mensagemLogin"] = "Ocorreu um erro inesperado, por favor tente novamente mais tarde, ou entre em contato conosco.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["mensagemLogin"] = "Ocorreu um erro inesperado, por favor tente novamente mais tarde, ou entre em contato conosco.";
                return View();
            }
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaSenha(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string PaginaTela = collection["btnRefeninir"];

                    //Realiza o controle de páginas
                    if (PaginaTela == "1")
                    {
                        string emailRedefinir = collection["emailRedefinir"];

                        /*********************************SELECT VALIDAÇÃO NO BANCO DE DADOS*********************************************/

                        StringBuilder ConsultaCadastro = new StringBuilder();
                        ConsultaCadastro.Append("SELECT * FROM TBL_SISTEMA_LOGIN WITH(NOLOCK)");
                        ConsultaCadastro.Append(string.Format(" WHERE EMAIL LIKE '%{0}%'", emailRedefinir));

                        DataBase selectConsultaCadastro = new DataBase();
                        dynamic CadastroExistente = selectConsultaCadastro.AcaoDataBase(ConsultaCadastro.ToString(), 4, 1, "SELECT");

                        /*Verifica Usuário*/

                        if (CadastroExistente.Count == 0)
                        {
                            TempData["mensagemRedefinirSenha"] = "Usuário incorreto! Esse usuário não existe. Tente novamente ou se cadastre em nosso sistema.";
                            TempData["PaginaTela"] = null;
                            return View();
                        }
                        else
                        {
                            /************GERA CODIGO ALEATORIO PARA ALTERACAO DA SENHA E INSERE NO BANCO**************/

                            Random codigo = new Random();
                            String codigoAleatorio = codigo.Next(0, 1000000).ToString("000000");

                            StringBuilder insertCodigo = new StringBuilder();
                            insertCodigo.Append("INSERT INTO TBL_SISTEMA_LOGIN_REDEFINIR_SENHA( EMAIL_REDEFINIR, CHAVE_ALEATORIA, DT_REGISTRO)");
                            insertCodigo.Append(string.Format(" VALUES('{0}', '{1}', '{2}')", emailRedefinir, codigoAleatorio, DateTime.Now));
                            DataBase InsertBanco = new DataBase();
                            InsertBanco.AcaoDataBase(insertCodigo.ToString(), 0, 1, "INSERT");

                            /*********************************ENVIO DE EMAIL*********************************************/
                            EnvioEmail enviar = new EnvioEmail();
                            enviar.EnviarEmail(CadastroExistente[1].ToString(), emailRedefinir, "Aqui está o código para registrar uma nova senha:<br><br><center><b>" + codigoAleatorio + "</b></center><br><br>Se você não solicitou nenhuma alteração de senha, por gentileza ignore este e-mail.");

                            //Guarda Dados em uma classe para uso futuro
                            NovaSenhaModel.EmailNovaSenha = emailRedefinir;
                            NovaSenhaModel.NomeNovaSenha = CadastroExistente[1].ToString();

                            TempData["PaginaTela"] = "1";
                            TempData["mensagemRedefinirSenha"] = "";

                            return View();
                        }
                    }
                    else if (PaginaTela == "2")
                    {
                        string codigoNovaSenha = collection["codigoNovaSenha"];
                        string senhaNova = collection["senhaNova"];
                        string senhaNovaRepetida = collection["senhaNovaRepetida"];

                        StringBuilder ConsultaChave = new StringBuilder();
                        ConsultaChave.Append("SELECT EMAIL_REDEFINIR,CHAVE_ALEATORIA,DT_REGISTRO=CAST(DT_REGISTRO AS DATE),HORA_CHAVE=RIGHT('00' + CONVERT(NVARCHAR(2), DATEPART(HOUR, GETDATE())), 2),MINUTO_CHAVE = DATEPART(MINUTE,DT_REGISTRO) FROM TBL_SISTEMA_LOGIN_REDEFINIR_SENHA WITH(NOLOCK)");
                        ConsultaChave.Append(" WHERE CHAVE_ALEATORIA = " + "'" + codigoNovaSenha + "'");
                        ConsultaChave.Append(" AND EMAIL_REDEFINIR = " + "'" + NovaSenhaModel.EmailNovaSenha.ToString() + "'");
                        DataBase SelectBanco = new DataBase();
                        dynamic DadosChave = SelectBanco.AcaoDataBase(ConsultaChave.ToString(), 5, 1, "SELECT");

                        //Valida Código e Email 
                        if (DadosChave.Count > 0)
                        {
                            var dataHoraAtual = DateTime.Now;
                            var dataAtual = dataHoraAtual.ToString("dd/MM/yyyy");
                            var horaAtual = dataHoraAtual.ToString("HH");
                            var MinutoAtual = dataHoraAtual.ToString("mm");

                            var dataRegistrada = DadosChave[2].Replace(" 00:00:00", "");
                            var horaRegistrado = DadosChave[3];
                            var minutoRegistrado = DadosChave[4];
                            var diferencaMinuto = Int32.Parse(MinutoAtual) - Int32.Parse(minutoRegistrado);

                            //Faz validação do tempo limite para troca de senha
                            if (dataRegistrada == dataAtual && horaAtual == horaRegistrado && diferencaMinuto < 10) {

                                //Valida a nova senha com a confirmação da nova senha
                                if (senhaNova == senhaNovaRepetida)
                                {
                                    /*********************************CRIPTOGRAFIA EM MD5*********************************************/

                                    var hashNovaSenha = Criptografia.CreateMD5(senhaNova);

                                    /*********************************INSERÇÃO NO BANCO DE DADOS*********************************************/

                                    StringBuilder updateNovaSenha = new StringBuilder();
                                    updateNovaSenha.Append("UPDATE TBL_SISTEMA_LOGIN");
                                    updateNovaSenha.Append(string.Format(" SET SENHA = '{0}'", hashNovaSenha.ToString()));
                                    updateNovaSenha.Append(string.Format(" WHERE EMAIL = '{0}'", NovaSenhaModel.EmailNovaSenha.ToString()));
                                    DataBase updateNovaSenhaBanco = new DataBase();
                                    updateNovaSenhaBanco.AcaoDataBase(updateNovaSenha.ToString(), 0, 1, "UPDATE");

                                    /*********************************ENVIO DE EMAIL*********************************************/

                                    EnvioEmail enviar = new EnvioEmail();
                                    enviar.EnviarEmail(NovaSenhaModel.NomeNovaSenha.ToString(), NovaSenhaModel.EmailNovaSenha.ToString(), "Sua alteração de senha foi realizada com sucesso!");

                                    TempData["mensagemConfirmacao"] = "Sua alteração de senha foi realizada com sucesso!";
                                    TempData["PaginaTela"] = null;
                                    TempData["codigoNovaSenha"] = "";
                                    TempData["senhaNova"] = "";
                                    TempData["senhaNovaRepetida"] = "";

                                    return Redirect("/Home");
                                }
                                else
                                {
                                    TempData["mensagemRedefinirSenha"] = "A confirmação de senha está incorreta! Por favor, insira a mesma senha nos dois campos.";
                                    TempData["PaginaTela"] = "1";
                                    TempData["codigoNovaSenha"] = codigoNovaSenha;
                                    return View();
                                }
                            }
                            else
                            {
                                TempData["mensagemRedefinirSenha"] = "O tempo limite para troca de senha foi expirado! Por favor, tente novamente.";
                                TempData["PaginaTela"] = null;
                                return View();
                            }
                        }
                        else
                        {
                            TempData["mensagemRedefinirSenha"] = "Código incorreto! Por favor, verifique em seu e-mail.";
                            TempData["PaginaTela"] = "1";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["mensagemRedefinirSenha"] = "Ocorreu um erro inesperado, por favor tente novamente mais tarde, ou entre em contato conosco.";
                        TempData["PaginaTela"] = null;
                        return View();
                    }
                }
                else
                {
                    TempData["PaginaTela"] = null;
                    TempData["mensagemRedefinirSenha"] = "Ocorreu um erro inesperado, por favor tente novamente mais tarde, ou entre em contato conosco.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["PaginaTela"] = null;
                TempData["mensagemRedefinirSenha"] = "Ocorreu um erro inesperado, por favor tente novamente mais tarde, ou entre em contato conosco.";
                return View();
            }


        }

        public ActionResult Sair()
        {
            //*************************DELETA COOKIES CASO EXISTA NA PAGINA*************//
            var key = "chave criptografada";
            var cookieCifrado = Criptografia.EncryptString(key, "verificado");

            var cookieCifradoIdUsuario = Criptografia.EncryptString(key, "ID_USUARIO");

            var cookieCifradoNmUsuario = Criptografia.EncryptString(key, "NM_USUARIO");

            var cookieCifradoEmailUsuario = Criptografia.EncryptString(key, "EMAIL_USUARIO");

            Response.Cookies.Delete(cookieCifrado);
            Response.Cookies.Delete(cookieCifradoIdUsuario);
            Response.Cookies.Delete(cookieCifradoNmUsuario);
            Response.Cookies.Delete(cookieCifradoEmailUsuario);

            /*DELETANDO DADOS DE USO SISTEMICO*/
            TempData["NomeUsuario"] = "";
            TempData["MenuAutomatico"] = "0";

            return Redirect("/Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
