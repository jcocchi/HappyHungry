using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite1;

public partial class Results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext CurrContext = HttpContext.Current;
            String description = CurrContext.Items["Description"].ToString();
            String link = CurrContext.Items["Link"].ToString();

            if (description != null)
            {
                //Response.Write(CurrContext.Items["Name"].ToString() + "<br/>");
                //Response.Write(CurrContext.Items["Address"].ToString());

                PResults.InnerText = description;
                SuggestedFood.ImageUrl = link;

                //PResults.InnerText = "Hello World";
                //SuggestedFood.ImageUrl = "http://newsrescue.com/wp-content/uploads/2015/04/happy-person.jpg";
            }
        } catch (Exception ex)
        {
            System.Diagnostics.Debug.Write("Results Page Load Error: " + ex.Message);
        }
    }

    public void BackBttn_Click(object sender, EventArgs e)
    {
        Server.Transfer("Default.aspx", true);
    }

}