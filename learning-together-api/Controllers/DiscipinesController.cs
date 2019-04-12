namespace learning_together_api.Controllers
{
    using Data;
    using Services;

    public class DisciplinesController : GetDataController<Discipline>
    {
        public DisciplinesController(IDisciplineService service) : base(service) { }
    }
}