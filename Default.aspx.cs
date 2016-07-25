using System;
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
using Newtonsoft.Json;

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

            // Parse results
            String emotion= ParseResults(results);
            System.Diagnostics.Debug.WriteLine("EMOTION: " + emotion);
        }
        catch (Exception exception)
        {
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

        return res1;
    }


    /*
     *
     * Sample response for reference when parsing out emotion/ score pairs
          [
            {
              "faceRectangle": {
                "left": 68,
                "top": 97,
                "width": 64,
                "height": 97
              },
              "scores": {
                "anger": 0.00300731952,
                "contempt": 5.14648448E-08,
                "disgust": 9.180124E-06,
                "fear": 0.0001912825,
                "happiness": 0.9875571,
                "neutral": 0.0009861537,
                "sadness": 1.889955E-05,
                "surprise": 0.008229999
              }
            }
          ]
     * 
     */
    static String ParseResults(String results)
    {
        //TODO: First, check if it is an invalid image, in this case it will return "[]"

        // JSON object to store API response
        EmotionSet emotions = new EmotionSet(); 

        // Populate a JSON object with the results of the API call
        results= results.TrimStart('[');
        results= results.TrimEnd(']');
        JsonConvert.PopulateObject(results, emotions);

        // Find top score 
        String topEmotion= emotions.getTopScore();
        System.Diagnostics.Debug.WriteLine("top emotion: " + topEmotion);

        return topEmotion;
    }
}
 