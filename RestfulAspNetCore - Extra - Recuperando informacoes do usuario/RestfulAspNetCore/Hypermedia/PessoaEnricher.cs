using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace RestfulAspNetCore.Hypermedia
{
    // HATEOAS (Hypermedia as the Engine of Application State) é a capacidade que a aplicação tem de prover o que
    // se pode fazer depois de uma requisição. É como se fosse uma espécie de menu. A API se torna autoexplicativa.
    public class PessoaEnricher : ObjectContentResponseEnricher<PessoaVo>
    {
        protected override Task EnrichModel(PessoaVo content, IUrlHelper urlHelper)
        {
            var path = "api/v1/Pessoas";

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.GET,
                Href = urlHelper.Link("DefaultApi", new { controller = path, id = content.Id }),
                Rel = RelationType.self, // Tipo de relacionamento.
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.POST,
                Href = urlHelper.Link("DefaultApi", new { controller = path }),
                Rel = RelationType.self, 
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.PUT,
                Href = urlHelper.Link("DefaultApi", new { controller = path }),
                Rel = RelationType.self, 
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink
            {
                Action = HttpActionVerb.DELETE,
                Href = urlHelper.Link("DefaultApi", new { controller = path, id = content.Id }),
                Rel = RelationType.self,
                Type = "int"
            });

            return null;
        }
    }
}
