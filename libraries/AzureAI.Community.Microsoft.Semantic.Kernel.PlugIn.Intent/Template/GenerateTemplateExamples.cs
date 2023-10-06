namespace AzureAI.Community.Microsoft.Semantic.Kernel.PlugIn.Intent.Template;

class GenerateTemplateExamples
{
    public static TemplateExamples[] CreateExamples()
    {
        TemplateExamples[] templateExamples = new TemplateExamples[4];

        var template1 = new TemplateExamples
        {
            Input = "I am planning a software development project for our new mobile app, scheduled to start next month.",
            Output = @"{
          ""text"": ""I am starting a new software development project for our mobile app next month"",
          ""intent"": {
            ""name"": ""Project Planning"",
            ""score"": 0.85
          },
          ""entities"": [
            {
              ""entity"": ""ProjectType"",
              ""type"": ""Development Project"",
              ""startPos"": 18,
              ""endPos"": 44,
              ""value"": ""software development project""
            },
            {
              ""entity"": ""ProjectName"",
              ""type"": ""Project Name"",
              ""startPos"": 49,
              ""endPos"": 61,
              ""value"": ""mobile app""
            },
            {
              ""entity"": ""StartDate"",
              ""type"": ""Start Date"",
              ""startPos"": 73,
              ""endPos"": 83,
              ""value"": ""Augest""
            }
          ]
        }"
        };

        var template2 = new TemplateExamples
        {
            Input = "I want to order a large pepperoni pizza for delivery, and I'd like it to be extra crispy.",
            Output = @"
                {
                  ""text"": ""I'd like to order a large extra crispy pepperoni pizza for delivery"",
                  ""intent"": {
                    ""name"": ""Food Ordering"",
                    ""score"": 0.92
                  },
                  ""entities"": [
                    {
                      ""entity"": ""FoodItem"",
                      ""type"": ""Pizza"",
                      ""startPos"": 31,
                      ""endPos"": 37,
                      ""value"": ""pepperoni pizza""
                    },
                    {
                      ""entity"": ""PizzaSize"",
                      ""type"": ""Pizza Size"",
                      ""startPos"": 13,
                      ""endPos"": 18,
                      ""value"": ""large""
                    },
                    {
                      ""entity"": ""PizzaCrust"",
                      ""type"": ""Pizza Crust"",
                      ""startPos"": 40,
                      ""endPos"": 53,
                      ""value"": ""extra crispy""
                    },
                    {
                      ""entity"": ""OrderType"",
                      ""type"": ""Order Type"",
                      ""startPos"": 45,
                      ""endPos"": 52,
                      ""value"": ""delivery""
                    }
                  ]
                }"
        };

        var template3 = new TemplateExamples
        {
            Input = "I am arranging a journey from New York to Chennai, departing this Sunday",
            Output = @"
            {
              ""text"": ""I am planning travel to NewYork to Chennai coming Sunday"",
              ""intent"": {
                ""name"": ""Travel Planning"",
                ""score"": 0.90
              },
              ""entities"": [
                {
                  ""entity"": ""Location"",
                  ""type"": ""Origin"",
                  ""startPos"": 25,
                  ""endPos"": 32,
                  ""value"": ""New York""
                },
                {
                  ""entity"": ""Location"",
                  ""type"": ""Destination"",
                  ""startPos"": 36,
                  ""endPos"": 42,
                  ""value"": ""Chennai""
                },
                {
                  ""entity"": ""Date"",
                  ""type"": ""Departure Date"",
                  ""startPos"": 43,
                  ""endPos"": 56,
                  ""value"": ""18.09.2023""
                }
              ]
            }"
        };

        var template4 = new TemplateExamples
        {
            Input = "I plan to secure a reservation for day after tomorrow dinner at 7 PM for a group of ten.",
            Output = @"
            {
              ""text"": ""I plan to secure a reservation for day after tomorrow dinner at 7 PM for a group of ten."",
              ""intent"": {
                ""name"": ""Reservation"",
                ""score"": 0.90
              },
              ""entities"": [
                {
                  ""entity"": ""Date"",
                  ""type"": ""Reservation Date"",
                  ""startPos"": 39,
                  ""endPos"": 48,
                  ""value"": ""17.09.2023""
                },
                {
                  ""entity"": ""Time"",
                  ""type"": ""Reservation Time"",
                  ""startPos"": 51,
                  ""endPos"": 56,
                  ""value"": ""7 PM""
                },
                {
                  ""entity"": ""Number"",
                  ""type"": ""Party Size"",
                  ""startPos"": 62,
                  ""endPos"": 67,
                  ""value"": ""10""
                }
              ]
            }"
        };

        templateExamples[0] = template1;
        templateExamples[1] = template2;
        templateExamples[2] = template3;
        templateExamples[3] = template4;

        return templateExamples;


    }

}