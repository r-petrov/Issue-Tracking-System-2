namespace IssueTrackingSystem2.Services.Data.Label
{
    using IssueTrackingSystem2.Services.Models;
    using System.Collections.Generic;


    public interface ILabelService
    {
        IEnumerable<LabelServiceModel> All();
    }
}
