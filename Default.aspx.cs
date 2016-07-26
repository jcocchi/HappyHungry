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
using WebSite1;

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
            String results = await MakeRequest(emotionPhoto);
            System.Diagnostics.Debug.WriteLine("RESULTS: " + results);

            // Parse results
            String emotion = ParseResults(results);
            System.Diagnostics.Debug.WriteLine("EMOTION: " + emotion);

            // Pick food to suggest
            String foodSuggestion = null;
            if (emotion == null)
            {
                System.Diagnostics.Debug.WriteLine("BAD PICTURE, COULDN'T FIND AN EMOTION");
            } else
            {
               foodSuggestion = MakeSuggestion(emotion);
            }
            System.Diagnostics.Debug.WriteLine("FOOD SUGGESTION FILEPATH: " + foodSuggestion);

        }
        catch (NullReferenceException ex)
        {
            System.Diagnostics.Debug.WriteLine("Null Reference: " + ex.Message);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
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
     * Sample success response for reference
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
        // JSON object to store API response
        EmotionSet emotions = new EmotionSet();
        // String to store top emotion
        String topEmotion = null;

        // Prepare result string for JSON conversion
        results = results.TrimStart('[');
        results= results.TrimEnd(']');

        // Make sure it was a good response, an invalid image will return "[]"
        if (results.Length > 1)
        {
            // Populate a JSON object with the results of the API call
            JsonConvert.PopulateObject(results, emotions);

            // Find top score 
            topEmotion = emotions.getTopScore();
            System.Diagnostics.Debug.WriteLine("top emotion: " + topEmotion);
        }

        return topEmotion;
    }


    static String MakeSuggestion(String emotion)
    {
        // First set the file path
        String suggestion = "C:\\Users\\jucocchi\\Documents\\Visual Studio 15\\WebSites\\WebSite1\\Pictures\\";

        // Now decide which image to use based on the emotion in the picture
        switch (emotion)
        {
            case "anger":
                suggestion += "steak.jpg";          // Steak
                break;
            case "contempt":                
                suggestion += "waffles.jpg";        // Waffles
                break;
            case "disgust":
                suggestion += "salad.jpg";          // Salad
                break;
            case "fear":
                suggestion += "lollipop.jpg";       // Scorpion Lollipop
                break;
            case "happiness":
                suggestion += "burger.jpg";         // Hamburger
                break;
            case "neutral":
                suggestion += "water.jpg";          // Water
                break;
            case "sadness":
                suggestion += "chocolate.jpg";      // Chocolate
                break;
            case "surprise":
                suggestion += "pizza.jpg";          // Breakfast Pizza
                break;
        }

        return suggestion;
    }
}
 