﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Caching;
using System.Web.Hosting;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;

namespace Telerik.Sitefinity.Frontend.Resources.Resolvers
{
    /// <summary>
    /// This class is responsible for resolving resource virtual files.
    /// </summary>
    /// <remarks>
    /// This class is registered as a virtual file resolver in Sitefinity's VirtualPathManager.
    /// </remarks>
    public class ResourceResolver : IVirtualFileResolver
    {
        /// <summary>
        /// Determines whether a file with the specified virtual path exists.
        /// </summary>
        /// <param name="virtualPath">The virtual path to check.</param>
        public virtual bool Exists(PathDefinition definition, string virtualPath)
        {
            virtualPath = this.RemoveParams(virtualPath);

            var resolverStrategy = ObjectFactory.Resolve<IResourceResolverStrategy>();
            return resolverStrategy.Exists(definition, virtualPath);
        }

        /// <summary>
        /// Creates a cache dependency based on the specified virtual paths.
        /// </summary>
        /// <param name="definition">The definition.</param>
        /// <param name="virtualPath">The path to the primary virtual resource.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Caching.CacheDependency"/> object for the specified virtual resources.
        /// </returns>
        public virtual CacheDependency GetCacheDependency(PathDefinition definition, string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            virtualPath = this.RemoveParams(virtualPath);

            var resolverStrategy = ObjectFactory.Resolve<IResourceResolverStrategy>();
            return resolverStrategy.GetCacheDependency(definition, virtualPath, virtualPathDependencies, utcStart);
        }

        /// <summary>
        /// Opens the the file with the specified virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path of the file to open.</param>
        public virtual Stream Open(PathDefinition definition, string virtualPath)
        {
            virtualPath = this.RemoveParams(virtualPath);

            var resolverStrategy = ObjectFactory.Resolve<IResourceResolverStrategy>();
            return resolverStrategy.Open(definition, virtualPath);
        }

        private string RemoveParams(string virtualPath)
        {
            var idx = virtualPath.IndexOf('#');
            if (idx == -1)
            {
                return virtualPath;
            }
            else
            {
                return virtualPath.Substring(0, idx);
            }
        }
    }
}
