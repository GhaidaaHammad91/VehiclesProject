namespace VehiclesProject.DTO
{
    public class Models
    {
        public int Make_ID { get; set; }
        public string Make_Name { get; set; }
        public int Model_ID { get; set; }
        public string Model_Name { get; set; }
    }

    public class APIResponse
    {
        public int Count { get; set; }
        public string? Message { get; set; }

        public string? SearchCriteria { get; set; }

        public List<Models> Results { get; set; }
    }

    public class ModelResponse
    {
        public List<string> Models { get; set;}

        public ModelResponse(List<string> models)
        {
            this.Models = models;
        }
    }
}
