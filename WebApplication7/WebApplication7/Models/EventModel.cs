using System.ComponentModel.DataAnnotations;
namespace WebApplication7.Models;

public class EventModel
{
    public enum EventStatus
    {
        [Display(Name = "Chưa bắt đầu")]
        NotStarted = 0,
        [Display(Name = "Đang diễn ra")]
        InProgress = 1,
        [Display(Name = "Đã kết thúc")]
        Completed = 2
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public EventStatus Status { get; set; }
}