﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Classes to parse JSON object
/// </summary>

namespace WebSite1
{
    public class FaceRectangle
    {
        private int left { get; set; }
        private int top { get; set; }
        private int width { get; set; }
        private int height { get; set; }
    }

    public class Scores
    {
        public double anger { get; set; }
        public double contempt { get; set; }
        public double disgust { get; set; }
        public double fear { get; set; }
        public double happiness { get; set; }
        public double neutral { get; set; }
        public double sadness { get; set; }
        public double surprise { get; set; }

        public String Max()
        {
            // Store all emotions for easier comparison
            double[] scores = new double[] { anger,
                                           contempt,
                                           disgust,
                                           fear,
                                           happiness,
                                           neutral,
                                           sadness,
                                           surprise };

            // Find top emotion
            int topEmotion = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > scores[topEmotion])
                {
                    topEmotion = i;
                }
            }

            // Return top emotion
            switch (topEmotion)
            {
                case 0:
                    return "anger";
                case 1:
                    return "contempt";
                case 2:
                    return "disgust";
                case 3:
                    return "fear";
                case 4:
                    return "happiness";
                case 5:
                    return "neutral";
                case 6:
                    return "sadness";
                case 7:
                    return "surprise";
            }

            return null;
        }
    }

    [JsonObject]
    public class EmotionSet
    {
        [JsonProperty(PropertyName = "faceRectangle")]
        public FaceRectangle faceRectangle { get; set; }
        [JsonProperty(PropertyName = "scores")]
        public Scores scores { get; set; } // Create a setter to populate all scores

        public String getTopScore()
        {
            return scores.Max();
        }
    }
}
