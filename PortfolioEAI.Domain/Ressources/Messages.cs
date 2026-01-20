
namespace PortfolioEAI.Domain.Ressources
{
    public static class Messages
    {
        //General error messages
        public const string EmptyId = "The {EntityType} Id cannot be empty.";
        public const string NullError = "Can't be null.";

        //Generic Repository error messages
        public const string AddError = "An error occurred while adding {EntityType}: {Message}";
        public const string AddNullError = "Attempted to add a null {EntityType} entity.";
        public const string DeleteError = "An error occurred while deleting {EntityType}: {Message}";
        public const string DeleteNullError = "Attempted to delete an {EntityType} with Id: {Id}.";
        public const string DeleteNullIdError = "Attempted to delete an {EntityType} with an empty Id.";
        public const string GetAllError = "An error occurred while retrieving all {EntityType}s: {Message}";
        public const string GetError = "An error occurred while retrieving {EntityType} with Id: {Id}: {Message}";
        public const string GetNullIdError = "Attempted to retrieve an {EntityType} with an empty Id.";
        public const string UpdateNullIdError = "Attempted to update an {EntityType} with an empty Id.";
        public const string UpdateNullError = "Attempted to update a null {EntityType} entity.";
        public const string UpdateError = "An error occurred while updating {EntityType}: {Message}";

        //General Repository messages
        public const string AddEntityInfo = "Adding a new {EntityType} with Id: {Id}";
        public const string DeleteAttemptEntityInfo = "Attempting to delete {EntityType} with Id: {Id}";
        public const string DeleteEntityInfo = "Delete {EntityType} with Id: {Id}";
        public const string GetEntityInfo = "Retrieving {EntityType} with Id: {Id}";
        public const string GetAllEntityInfo = "Retrieving all {EntityType}s";
        public const string UpdateEntityInfo = "Updating {EntityType} with Id: {Id}";
        public const string UpdateNotFound = "The {EntityType} with Id: {Id} not found for update.";


        //General services error messages
        public const string AddDTOError = "An error occurred while adding {DTIType}: {Message}";
        public const string AddDTONullError = "Attempted to add a null {DTOType} DTO.";

        public const string GetAllDTOError = "An error occurred while fetching all {DTOType}s: {Message}";

        public const string GetDTOError = "An error occurred while fetching {DTOType} with Id: {Id}: {Message}";
        public const string GetNullIdDTOError = "Attempted to fetching a {DTOType} with an empty Id.";

        public const string DeleteDTOEmptyIdError = "Attempted to delete an {DtoType} with an empty Id.";
        public const string DeleteDTOError = "An error occurred while deleting {DtoType}: {Message}";


        public const string UpdateDTONullError = "Attempted to update a null {DtoType} entity.";
        public const string UpdateDTOError = "An error occurred while updating {DTOType} with Id: {Id}: {Message}";

        //General services messages
        public const string AddDTOInfo = "Adding a new {DTOType}: {Id}";
        public const string AddNotFound = "The {EntityType} with Id: {Id} not found for adding in {EntityType1} with Id:";

        public const string DeleteAttemptDTOInfo = "Attempting to delete {DTOType} with Id: {Id}";
        public const string DeleteDTONotFound = "The {DTOType} with Id: {Id} not found for deletion.";
        public const string DeleteDTOInfo = "Delete {DTOType} with Id: {Id}";
        public const string GetDTOInfo = "Retrieving {DTOType} with Id: {Id}";
        public const string GetAllDTOInfo = "Fetching all {DTOType}s";
        public const string UpdateDTOInfo = "Updating {DTOType} with Id: {Id}";
        public const string UpdateDTOInEntityInfo = "Updating {DTOType} with Id: {Id} in {EntityType} entity.";
        public const string UpdateDTONotFound = "The {DTOType} with Id: {Id} not found for update.";


    }
}