using System.Collections.Generic;

namespace CGC
{
    public class DataPoint
    {
        /// <summary>
        ///     Dictionary with Name-Value mapping 
        /// </summary>
        public Dictionary<string, string> Points { get; }

        private DataPoint(Dictionary<string, string> points)
        {
            Points = points;
        }
        
        /// <summary>
        ///     Create DataPoint instance from fields
        /// </summary>
        /// <param name="fields">Fields array</param>
        /// <param name="propertyPositions">Property name and position in fields array</param>
        /// <returns>DataPoint instance</returns>
        public static DataPoint FromFields(string[] fields, Dictionary<string,int> propertyPositions)
        {   
            var points = new Dictionary<string,string>();

            foreach (var property in propertyPositions.Keys)
            {
                int propertyIndex = propertyPositions[property];
                points.Add(property, fields[propertyIndex]);
            }

            return new DataPoint(points);
        }
        
        /// <summary>
        ///     Get property value
        /// </summary>
        /// <param name="property">Property name</param>
        /// <returns>Value</returns>
        public string GetPropertyValue(string property)
        {
            bool exist = Points.TryGetValue(property,out var propertyValue);
            if (!exist) return null;
            return propertyValue;
        }
    }
}