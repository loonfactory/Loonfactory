// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to AuthenticationProperties.cs of dotnet/aspnetcore.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Loonfactory.Translate
{
  /// <summary>
  /// Dictionary used to store state values about the translate session.
  /// </summary>
  public class TranslateProperties
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateProperties"/> class.
    /// </summary>
    public TranslateProperties()
        : this(items: null, parameters: null)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateProperties"/> class.
    /// </summary>
    /// <param name="items">State values dictionary to use.</param>
    [JsonConstructor]
    public TranslateProperties(IDictionary<string, string?> items)
        : this(items, parameters: null)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateProperties"/> class.
    /// </summary>
    /// <param name="items">State values dictionary to use.</param>
    /// <param name="parameters">Parameters dictionary to use.</param>
    public TranslateProperties(IDictionary<string, string?>? items, IDictionary<string, object?>? parameters)
    {
      Items = items ?? new Dictionary<string, string?>(StringComparer.Ordinal);
      Parameters = parameters ?? new Dictionary<string, object?>(StringComparer.Ordinal);
    }

    /// <summary>
    /// Return a copy.
    /// </summary>
    /// <returns>A copy.</returns>
    public TranslateProperties Clone()
        => new TranslateProperties(
            new Dictionary<string, string?>(Items, StringComparer.Ordinal),
            new Dictionary<string, object?>(Parameters, StringComparer.Ordinal));

    /// <summary>
    /// State values about the translate session.
    /// </summary>
    public IDictionary<string, string?> Items { get; }

    /// <summary>
    /// Collection of parameters that are passed to the translate handler. These are not intended for
    /// serialization or persistence, only for flowing data between call sites.
    /// </summary>
    [JsonIgnore]
    public IDictionary<string, object?> Parameters { get; }

    /// <summary>
    /// Get a string value from the <see cref="Items"/> collection.
    /// </summary>
    /// <param name="key">Property key.</param>
    /// <returns>Retrieved value or <c>null</c> if the property is not set.</returns>
    public string? GetString(string key)
    {
      return Items.TryGetValue(key, out var value) ? value : null;
    }

    /// <summary>
    /// Set or remove a string value from the <see cref="Items"/> collection.
    /// </summary>
    /// <param name="key">Property key.</param>
    /// <param name="value">Value to set or <see langword="null" /> to remove the property.</param>
    public void SetString(string key, string? value)
    {
      if (value != null)
      {
        Items[key] = value;
      }
      else
      {
        Items.Remove(key);
      }
    }

    /// <summary>
    /// Get a parameter from the <see cref="Parameters"/> collection.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <param name="key">Parameter key.</param>
    /// <returns>Retrieved value or the default value if the property is not set.</returns>
    public T? GetParameter<T>(string key)
        => Parameters.TryGetValue(key, out var obj) && obj is T value ? value : default;

    /// <summary>
    /// Set a parameter value in the <see cref="Parameters"/> collection.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <param name="key">Parameter key.</param>
    /// <param name="value">Value to set.</param>
    public void SetParameter<T>(string key, T value)
        => Parameters[key] = value;

    /// <summary>
    /// Get a nullable <see cref="bool"/> from the <see cref="Items"/> collection.
    /// </summary>
    /// <param name="key">Property key.</param>
    /// <returns>Retrieved value or <see langword="null" /> if the property is not set.</returns>
    protected bool? GetBool(string key)
    {
      if (Items.TryGetValue(key, out var value) && bool.TryParse(value, out var boolValue))
      {
        return boolValue;
      }
      return null;
    }

    /// <summary>
    /// Set or remove a <see cref="bool"/> value in the <see cref="Items"/> collection.
    /// </summary>
    /// <param name="key">Property key.</param>
    /// <param name="value">Value to set or <see langword="null" /> to remove the property.</param>
    protected void SetBool(string key, bool? value)
    {
      if (value.HasValue)
      {
        Items[key] = value.GetValueOrDefault().ToString();
      }
      else
      {
        Items.Remove(key);
      }
    }
  }
}
