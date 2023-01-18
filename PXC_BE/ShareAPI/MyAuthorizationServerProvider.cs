
using System;
using System.Data;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using FTS.Base.Systems;
using System.Data.Common;

namespace FTS.ShareAPI
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var formAsync = await context.Request.ReadFormAsync();
            context.Validated(); // 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var formAsync = await context.Request.ReadFormAsync();
            /*Nam lam viec hien tai*/
            string workingYear = DateTime.Now.Year.ToString();
            if (formAsync["WorkingYear"] != null)
            {
                workingYear = formAsync["WorkingYear"];
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            FTSMain mFTSMain = StaticMain.FTSMain();
            string sql = "SELECT * FROM SEC_USER WHERE USER_ID = @USER_ID";// '" + context.UserName + "'";
            DbCommand cmd = mFTSMain.DbMain.GetSqlStringCommand(sql);
            mFTSMain.DbMain.AddInParameter(cmd, "USER_ID", DbType.String, context.UserName);
            DataTable sec_user = mFTSMain.DbMain.LoadDataTable(cmd, "SEC_USER");
            if (sec_user.Rows.Count > 0)
            {
                string password = FTS.Base.Utilities.FunctionsBase.Decrypt(sec_user.Rows[0]["USER_PASSWORD"].ToString());
                if (context.Password == password)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, sec_user.Rows[0]["USER_GROUP_ID"].ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, sec_user.Rows[0]["USER_ID"].ToString()));
                    identity.AddClaim(new Claim("OrganizationID", sec_user.Rows[0]["ORGANIZATION_ID"].ToString()));
                    identity.AddClaim(new Claim("OrganizationName", sec_user.Rows[0]["ORGANIZATION_ID"].ToString()));
                    identity.AddClaim(new Claim("UserID", sec_user.Rows[0]["USER_ID"].ToString()));
                    identity.AddClaim(new Claim("UserGroupID", sec_user.Rows[0]["USER_GROUP_ID"].ToString()));
                    identity.AddClaim(new Claim("WorkingYear", workingYear));
                    context.Validated(identity);
                }
                else
                {
                    string str = "Your login details are incorrect. Please check your login information.";
                    context.SetError("400", str);
                    return;
                }
            }
            else
            {
                string str = "Your login details are incorrect. Please check your login information.";
                context.SetError("400", str);
                return;
            }
        }
    }
}