// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to AuthenticationScheme.cs of dotnet/aspnetcore.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Loonfactory.Translate
{
    /// <summary>
    /// TranslateSchemes assign a name to a specific <see cref="ITranslateHandler"/>
    /// handlerType.
    /// </summary>
    public class TranslateScheme
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TranslateScheme"/>.
        /// </summary>
        /// <param name="name">The name for the translate scheme.</param>
        /// <param name="displayName">The display name for the translate scheme.</param>
        /// <param name="handlerType">The <see cref="ITranslateHandler"/> type that handles this scheme.</param>
        public TranslateScheme(string name, string? displayName, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type handlerType)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (handlerType == null)
            {
                throw new ArgumentNullException(nameof(handlerType));
            }
            if (!typeof(ITranslateHandler).IsAssignableFrom(handlerType))
            {
                throw new ArgumentException("handlerType must implement ITransalteHandler.");
            }

            Name = name;
            HandlerType = handlerType;
            DisplayName = displayName;
        }

        /// <summary>
        /// The name of the translate scheme.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The display name for the scheme. Null is valid and used for non user facing schemes.
        /// </summary>
        public string? DisplayName { get; }

        /// <summary>
        /// The <see cref="ITranslateHandler"/> type that handles this scheme.
        /// </summary>
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        public Type HandlerType { get; }
    }
}
