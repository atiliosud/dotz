using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Dotz.Core.Domain.Models;
using Dotz.Core.Domain.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Dotz.Services
{
  public static class UserPointsControlService
  {
    public static List<UserPointsControl> GenerateUserPoints([FromServices] IUserPointsControlRepository userPointsRepository,
    [FromServices] IProductRepository productRepository,
                  Order order)
    {
      List<UserPointsControl> userPointsList = new List<UserPointsControl>();
      foreach (Product item in order.Products)
      {
        //Localizo o produto
        Product product = productRepository.Get().AsNoTracking().Where(x => x.Id == item.Id).FirstOrDefault();
        if (product != null){
          //Se o produto é válido para ser resgatado
          if (product.AvaiableToDischarge)
          {
            //Selecionar o total de pontos já acumulados referente ao produto
            userPointsList = userPointsRepository.Get().AsNoTracking().Where(x => x.ProductId == product.Id &&
                                              x.UserId == order.UserId).ToList();

            int accumulatedPoints = 0;
            if (userPointsList.Count > 0)
              //Verificao de total de pontos
              accumulatedPoints = userPointsList.Sum(x => x.GeneratedPoints);

            //Acumulo novos pontos aos já existentes
            UserPointsControl userPoints = new UserPointsControl();
            userPoints.UserId = order.UserId;
            userPoints.ProductId = product.Id;
            userPoints.GeneratedPoints = product.PointsToAdd;
            accumulatedPoints += userPoints.GeneratedPoints;

            //Pode resgatar
            if (accumulatedPoints > product.PointsToDischarge)
              userPoints.CanDischarge = true;
            else
              userPoints.CanDischarge = false;

            //Acumulo novos pontos aos já existentes
            userPointsList.Add(userPoints);
          }
        }
      }
      return userPointsList;
    }
  }
}