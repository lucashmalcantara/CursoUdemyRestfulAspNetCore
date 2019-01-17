using Microsoft.AspNetCore.Mvc;
using RestfulAspNetCore.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace RestfulAspNetCore.Hypermedia
{
    public class LivroEnricher : ObjectContentResponseEnricher<LivroVo>
    {
        protected override Task EnrichModel(LivroVo content, IUrlHelper urlHelper)
        {
            var path = "api/v1/Livros";

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
