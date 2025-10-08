using System;

namespace FCG.Application.Responses
{
    public class CreateGameResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
