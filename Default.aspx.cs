﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Common;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public async void ShowResultsBttn_Click(object sender, EventArgs e)
    {
        // Save image url
        String emotionPhoto = EmotionPhoto.Text;

        try
        {
            // Make API request
            String results= await MakeRequest(emotionPhoto);

            System.Diagnostics.Debug.WriteLine("RESULTS: " + results);
        }
        catch (Exception exception)
        {
            System.Diagnostics.Debug.WriteLine("Detection failed. Please make sure that you have the right subscription key and proper URL to detect.");
            System.Diagnostics.Debug.WriteLine(exception.Message);
        }
    }


    static async Task<String> MakeRequest(String photo)
    {
        var client = new HttpClient();
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        // Request headers
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "6470040a779d442b93118357ccff41ce");

        var uri = "https://api.projectoxford.ai/emotion/v1.0/recognize" + queryString;

        HttpResponseMessage response;
        
        // Format Body request
        String picURL= "\"" + photo + "\"";
        String format = "\"url\":";
        String requestBody= String.Format("{0} {1}", format, picURL);
        requestBody = "{" + requestBody + "}";
        byte[] byteData = Encoding.UTF8.GetBytes(requestBody);

        // Send request
        using (var content = new ByteArrayContent(byteData))
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response = await client.PostAsync(uri, content).ConfigureAwait(false);
        }

        // Convert response to string
        HttpContent results = response.Content;
        var res1 = await results.ReadAsStringAsync();
        var res2 = Newtonsoft.Json.JsonConvert.DeserializeObject(res1);
        String res3 = res2.ToString();

        return res3;
    }
}