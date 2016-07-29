using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
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
        //Server.Transfer("Results.aspx", true);


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

            // Display error message to the user if no emotion was found
            if (emotion == null)  
            {
                System.Diagnostics.Debug.WriteLine("BAD PICTURE, COULDN'T FIND AN EMOTION");

                PResults.InnerText = "Oops something went wrong! Please make sure that you submitted the correct image link and that your face is both promienent in the image and unobstructed. Submit another link to try again!";
            }
            // Otherwise pick the food to suggest and display it to the user
            else
            {
                Suggestion suggestion = MakeSuggestion(emotion);
                System.Diagnostics.Debug.WriteLine("FOOD SUGGESTION DESCRIPTION: " + suggestion.description);
                System.Diagnostics.Debug.WriteLine("FOOD SUGGESTION FILEPATH: " + suggestion.link);

                PResults.InnerText = suggestion.description;
                SuggestedFood.ImageUrl = suggestion.link;
            }
                       
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


    static Suggestion MakeSuggestion(String emotion)
    {
        String link = null;
        String description = null;

        // Decide which image to use based on the emotion in the picture
        switch (emotion)
        {
            case "anger":
                link = "http://swansonquotes.com/wp-content/uploads/s03-ep12-hometown1-1000x500.jpg";
                description = "It looks like you're feeling angry. Let your anger out by cutting up a nice juicy steak!";
                break;
            case "contempt":
                link = "http://cdn2.thegrindstone.com/wp-content/uploads/2012/11/leslie-knope-waffles.jpg";
                description = "What's a better way to stop feeling contempt than to eat a huge plate of waffles?";
                break;
            case "disgust":
                link = "http://www.eatmedaily.com/wordpress/wp-content/uploads/2009/03/simpsons-food-faces.jpg";
                description = "No one blames you for feeling disgusted when you're so hungry, try a nice salad to turn your day around!";
                break;
            case "fear":
                link = "https://s-media-cache-ak0.pinimg.com/564x/e0/52/05/e05205817d704c30d3b88c35e7c8e4e2.jpg";
                description = "It looks like you're feeling a little scared. Face your fears by eating a scorpion lollipop!";
                break;
            case "happiness":
                link = "http://i.onionstatic.com/avclub/5745/19/16x9/960.jpg";
                description = "There is no better way to celebrate feeling happy than by enjoying a gigantic hamburger!";
                break;
            case "neutral":
                link = "http://www.shapeme.com.au/blog/wp-content/uploads/2015/05/water-glass-drinking-58690800888_xlarge.jpeg";
                description = "You're feeling neutral so you should stick to plain foods...like water.";
                break;
            case "sadness":
                link = "https://i.ytimg.com/vi/KIBy-thiS3I/hqdefault.jpg";
                description = "No one likes being sad, have some chocolate to turn your day around!";
                break;
            case "surprise":
                link = "http://i.huffpost.com/gadgets/slideshows/363949/slide_363949_4114801_free.jpg";
                description = "You look suprised, have a fun fusion food like a breakfast pizza!";
                break;
        }

        // Store suggestion
        Suggestion suggestion = new Suggestion(link, description);

        return suggestion;
    } 
}
 