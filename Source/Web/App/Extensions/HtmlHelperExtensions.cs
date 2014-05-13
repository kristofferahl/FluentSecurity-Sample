using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Web.App.Extensions
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString Navigation(this HtmlHelper htmlHelper, Func<IEnumerable<MvcHtmlString>> items, object attributes = null, string listElement = "ul")
		{
			var nonEmptyItems = items().Where(x => !String.IsNullOrWhiteSpace(x.ToHtmlString())).ToList();
			if (nonEmptyItems.Any() == false) return MvcHtmlString.Empty;

			var innerHtml = nonEmptyItems.Aggregate(String.Empty, (current, item) => String.Concat(current, item.ToHtmlString()));

			var list = new TagBuilder(listElement);
			list.AddAttributes(attributes);
			list.InnerHtml = innerHtml;
			
			return MvcHtmlString.Create(list.ToString());
		}

		public static MvcHtmlString NavigationLink<TController>(this HtmlHelper htmlHelper,
			Expression<Func<TController, ActionResult>> actionExpression,
			string innerHtml,
			string wrapperElement = null,
			object htmlAttributes = null,
			bool markSelectedWhenActive = true,
			bool markSelectedWhenExactMatchOnly = false,
			bool alwaysDisplayInnerHtml = false
			) where TController : IController
		{
			return htmlHelper.NavigationLink(actionExpression, null, innerHtml, wrapperElement, htmlAttributes, markSelectedWhenActive, markSelectedWhenExactMatchOnly, alwaysDisplayInnerHtml);
		}

		public static MvcHtmlString NavigationLink<TController>(this HtmlHelper htmlHelper,
			Expression<Func<TController, ActionResult>> actionExpression,
			object values,
			string innerHtml,
			string wrapperElement = null,
			object htmlAttributes = null,
			bool markSelectedWhenActive = true,
			bool markSelectedWhenExactMatchOnly = false,
			bool alwaysDisplayInnerHtml = false
			) where TController : IController
		{
			var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
			var url = urlHelper.Action(actionExpression, values);

			return htmlHelper.NavigationLink(url, innerHtml, wrapperElement, htmlAttributes, markSelectedWhenActive, markSelectedWhenExactMatchOnly, alwaysDisplayInnerHtml);
		}

		private static MvcHtmlString NavigationLink(this HtmlHelper htmlHelper,
			string url,
			string innerHtml,
			string wrapperElement = null,
			object htmlAttributes = null,
			bool markSelectedWhenActive = true,
			bool markSelectedWhenExactMatchOnly = false,
			bool alwaysDisplayInnerHtml = false
			)
		{
			if (string.IsNullOrEmpty(url))
		        return alwaysDisplayInnerHtml ? MvcHtmlString.Create(innerHtml) : MvcHtmlString.Empty;

			var tagBuilder = new TagBuilder("a");
			tagBuilder.MergeAttribute("href", url);
			tagBuilder.AddAttributes(htmlAttributes);

			Func<string, HtmlHelper, bool> urlIsCurrentPathMatch = UrlMatchesCurrentPath;
			if (markSelectedWhenExactMatchOnly) urlIsCurrentPathMatch = UrlMatchesCurrentPathExactly;

			if (markSelectedWhenActive && urlIsCurrentPathMatch(url, htmlHelper) && string.IsNullOrEmpty(wrapperElement))
				tagBuilder.InnerHtml = String.Format("<strong>{0}</strong>", innerHtml);
			else
				tagBuilder.InnerHtml = innerHtml;
			
			var navigationLink = tagBuilder.ToString(TagRenderMode.Normal);

			if (!string.IsNullOrEmpty(wrapperElement))
			{
				var wrapperElementBuilder = new TagBuilder(wrapperElement);
				if (markSelectedWhenActive && urlIsCurrentPathMatch(url, htmlHelper))
					wrapperElementBuilder.InnerHtml = String.Format("<strong>{0}</strong>", navigationLink);
				else
					wrapperElementBuilder.InnerHtml = navigationLink;
				navigationLink = wrapperElementBuilder.ToString(TagRenderMode.Normal);
			}

			return MvcHtmlString.Create(navigationLink);
		}

		private static bool UrlMatchesCurrentPath(string url, HtmlHelper htmlHelper)
		{
			var currentPath  = GetCurrentPath(htmlHelper);
			return (url == currentPath || currentPath.StartsWith(url + "/"));
		}

		private static bool UrlMatchesCurrentPathExactly(string url, HtmlHelper htmlHelper)
		{
			var currentPath = GetCurrentPath(htmlHelper);
			return (url == currentPath);
		}

		private static string GetCurrentPath(HtmlHelper htmlHelper)
		{
			if (htmlHelper == null ||
			    htmlHelper.ViewContext == null ||
			    htmlHelper.ViewContext.HttpContext == null ||
			    htmlHelper.ViewContext.HttpContext.Request == null)
			{
				return null;
			}

			return htmlHelper.ViewContext.HttpContext.Request.Path;
		}
	}
}