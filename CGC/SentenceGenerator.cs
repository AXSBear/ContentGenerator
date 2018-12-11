using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace CGC
{
    public class SentenceGenerator
    {
        /// <summary>
        ///     Sentences with placeholders
        /// </summary>
        public static readonly string[] SentencesPool = new string[] 
            {
                "[Analyst Firm] analyst [Analyst Name] is out today with a bullish research note on [Company], upgrading the stock two notches -- all the way from [Previous Rating] to [Rating] -- with a price target of $[Target] a share, which implies a [Percent]% upside/downside from current levels.",
                "[Analyst Firm] analyst [Analyst Name] is singing [Company]'s praises, upgrading the stock from [Previous Rating] to [Rating] with a price target of $[Target]. That implies [Company] shares could rise/fall [Percent] in 12 months.", 
                "[Analyst Firm] analyst [Analyst Name] released a bullish note on shares of [Company] suggesting that expectations for the company are more reasonable now than earlier in the year. The analyst upgrades the stock to [Rating] from [Previous Rating], with a price target of $[Target], which represents a potential upside/downside of [Percent]% from its current share price.", 
                "[Analyst Firm] analyst [Analyst Name] just announced a big upgrade on [Company] stock. The reason: From current share price of $[Current price] a share, the analyst believes the stock could rise nearly [Percent]% over the next 12 months.", 
                "[Company] received a vote of confidence from Wall Street as [Analyst Firm] analyst [Analyst Name] reiterated his [Rating] rating on the company, while also reaffirming his 12 month price target of $[Target] — a [Percent]% upside/downside from current levels.", 
                "In a recent note, [Analyst Firm] analyst [Analyst Name] backed the firm’s [Rating] rating for [Company], placing a price target of $[Target] (a [Percent]% upside/downside potential) for the company’s stock."
            };
        
        /// <summary>
        ///     Generate sentence with data point from TSV file 
        /// </summary>
        /// <param name="tsvPath"></param>
        /// <returns></returns>
        public static string Generate(string tsvPath)
        {
            Random random = new Random();
            List<DataPoint> dataPoints = ReadDataPoints(tsvPath).ToList();

            string randomSentence = SentencesPool[random.Next(0, SentencesPool.Length)];
            DataPoint randomDataPoints = dataPoints[random.Next(0, dataPoints.Count)];
            
            string paragraph = CreateParagraph(randomSentence, randomDataPoints);
            
            return paragraph;
        }

        /// <summary>
        ///     Read data point from TSV file
        /// </summary>
        /// <param name="tsvPath">Path to TSV file</param>
        /// <param name="hasHeaders">Has headers or not</param>
        /// <returns>List of data point from TSV file</returns>
        private static IEnumerable<DataPoint> ReadDataPoints(string tsvPath, bool hasHeaders = true)
        {
            var reader = new TextFieldParser(tsvPath)
            {
                Delimiters = new [] {"\t"}
            };

            var properties = new Dictionary<string,int>();
            
            if (hasHeaders)
            {
                properties = ReadPropertiesFromFields(reader.ReadFields());
            }
            
            while (!reader.EndOfData)
            {
                yield return DataPoint.FromFields(reader.ReadFields(), properties);
            }
        }

        
        /// <summary>
        ///     Create properies-index dictionary 
        /// </summary>
        /// <param name="fields">Fields from TSV file</param>
        /// <returns></returns>
        private static Dictionary<string, int> ReadPropertiesFromFields(string[] fields)
        {
            var properties = new Dictionary<string,int>();

            for (int i = 0; i < fields.Length; i++)
            {
                properties.Add(fields[i],i);
            }

            return properties;
        }
        
        /// <summary>
        ///     Create paragraph
        /// </summary>
        /// <param name="paragraph">Source paragraph</param>
        /// <param name="dataPoint">Relevant data points</param>
        /// <returns>Generating paragraph</returns>
        private static string CreateParagraph(string paragraph, DataPoint dataPoint)
        {
            return paragraph.CustomFormat(dataPoint.Points);
        }
    }
}