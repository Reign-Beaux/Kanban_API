using Kanban.Application.Common.Models;
using Kanban.Application.Common.Statics;
using Kanban.Application.Common.Utils;
using Kanban.Application.Interfaces;
using Kanban.Application.Validators.GroupProjects;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class GroupProjectService : BaseService, IGroupProjectService
  {
    private readonly GroupProjectValidator _validator;

    public GroupProjectService(
      IUnitOfWork unitOfWork,
      ExceptionsLogger logger,
      GroupProjectValidator validator) : base(unitOfWork, logger)
    {
      _validator = validator;
    }

    public async Task<ResponseData<List<GroupProject>>> GetGroupProjects()
    {
      var response = new ResponseData<List<GroupProject>>();
      try
      {
        response.Data = await _unitOfWork.GroupProjectRepository.GetGroupProjects();
        response.Message =
          response.Data.Count > 0
          ? ReplyMessages.QUERY_SUCCESS
          : ReplyMessages.QUERY_EMPTY;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.QUERY_FAILED;
        _logger.SetException("Error al obtener grupos de proyectos: " + ex.Message);
      }

      return response;
    }

    public async Task<ResponseData<GroupProject>> GetGroupProjectById(int id)
    {
      var response = new ResponseData<GroupProject>();
      try
      {
        var groupProject = await _unitOfWork.GroupProjectRepository.GetGroupProjectById(id);

        if (groupProject is null)
        {
          response.Status = StatusResponse.NOT_FOUND;
          response.Message = ReplyMessages.RECORD_NOT_FOUND;
        }
        else
        {
          response.Data = groupProject;
          response.Message = ReplyMessages.QUERY_SUCCESS;
        }
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.QUERY_FAILED;
        _logger.SetException("Error al obtener grupo de proyecto por id: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> InsertGroupProject(GroupProject groupProject)
    {
      var response = new Response();
      var validationResult = await _validator.ExecuteValidateGroupProject(groupProject);
      if (!validationResult.IsValid)
      {
        response.NotValid(validationResult.Errors);
        return response;
      }

      try
      {
        await _unitOfWork.GroupProjectRepository.InsertGroupProject(groupProject);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.SAVE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al insertar grupo de proyecto: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> UpdateGroupProject(GroupProject groupProject)
    {
      var response = new Response();
      var currentGroupProject = await _unitOfWork.GroupProjectRepository.GetGroupProjectById(groupProject.Id);

      if (currentGroupProject is null)
      {
        response.Status = StatusResponse.NOT_FOUND;
        response.Message = ReplyMessages.RECORD_NOT_FOUND;
        return response;
      }

      var validationResult = await _validator.ExecuteValidateGroupProject(groupProject);
      if (!validationResult.IsValid)
      {
        response.NotValid(validationResult.Errors);
        return response;
      }

      try
      {
        await _unitOfWork.GroupProjectRepository.UpdateGroupProject(groupProject);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.UPDATE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al actualizar grupo de proyecto: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> DeleteGroupProject(int id)
    {
      var response = new Response();
      var currentGroupProject = await _unitOfWork.GroupProjectRepository.GetGroupProjectById(id);

      if (currentGroupProject is null)
      {
        response.Status = StatusResponse.NOT_FOUND;
        response.Message = ReplyMessages.RECORD_NOT_FOUND;
        return response;
      }

      try
      {
        await _unitOfWork.GroupProjectRepository.DeleteGroupProject(id);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.DELETE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al eliminar grupo de proyecto: " + ex.Message);
      }

      return response;
    }
  }
}
