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
            if (Context.Items.Contains("Description"))
            {
                String description = Context.Items["Description"].ToString();
                String link = Context.Items["Link"].ToString();

                if (description != null)
                {
                    PResults.InnerText = description;
                    SuggestedFood.ImageUrl = link;
                }
            } else // Page should never load unless it is displaying results from a query
            {
                Response.Redirect("Default.aspx");
            }
        } catch (NullReferenceException ex)
        {
            System.Diagnostics.Debug.Write("Results Page Null Reference Exception: " + ex.Message);

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Write("Results Page Exception: " + ex.Message);
        }
    }

    public void BackBttn_Click(object sender, EventArgs e)
    {
        try
        {
            System.Diagnostics.Debug.Write("SHOULD REDIRECT TO THE DEFAULT PAGE");
            Response.Redirect("Default.aspx");
            //Server.Transfer("Default.aspx");
        } catch (InvalidOperationException ex)
        {
            System.Diagnostics.Debug.Write("Back Button Invalid Operation Exception: " + ex.Message);
        } catch (Exception ex)
        {
            System.Diagnostics.Debug.Write("Back Button Exception: " + ex.Message);
        }
    }

}