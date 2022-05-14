namespace Med.FeatureFlags;

/// <summary>
/// A base implementation of <see cref="IFeatureFlag" />.
/// </summary>
/// <seealso cref="IFeatureFlag" />
/// <seealso cref="INotifyPropertyChanging" />
/// <seealso cref="INotifyPropertyChanged" />
/// <seealso cref="IDisposable" />
public abstract class BaseFeatureFlag :
    IFeatureFlag,
    INotifyPropertyChanging,
    INotifyPropertyChanged,
    IDisposable
{
    private bool _enabled;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseFeatureFlag" /> class.
    /// </summary>
    /// <param name="id">An identifier for this feature flag instance.</param>
    protected BaseFeatureFlag(string id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    /// <summary>
    /// Gets the identifier for this feature flag instance.
    /// </summary>
    public virtual string Id { get; }

    /// <summary>
    /// Gets of sets a value indicating whether this feature flag is enabled.
    /// </summary>
    public bool Enabled
    {
        get => _enabled;
        protected set => SetProperty(ref _enabled, value, onChanged: () => OnEnabledChanged(_enabled));
    }

    /// <inheritdoc />
    public event EventHandler<bool> EnabledChanged;

    /// <summary>
    ///    Raises the <see cref="EnabledChanged" /> event.
    /// </summary>
    /// <param name="enabled">The new value of the property <see cref="Enabled"/>.</param>
    protected virtual void OnEnabledChanged(bool enabled)
    {
        EnabledChanged?.Invoke(this, enabled);
    }

    /// <summary>
    ///     Sets <paramref name="backingStore" /> as a new value for <paramref name="propertyName" /> and performs
    ///     notifications and actions accordingly.
    /// </summary>
    /// <typeparam name="T">Type of the property getting set.</typeparam>
    /// <param name="backingStore">Variable holding the value.</param>
    /// <param name="value">New value to be set.</param>
    /// <param name="propertyName">Name of the property getting set.</param>
    /// <param name="onChanging">Optional operation to be performed right before setting the new value.</param>
    /// <param name="onChanged">Optional operation to be performed right after the new value as been set.</param>
    /// <returns><c>true</c> if the value is set successfully. Otherwise, <c>false</c>.</returns>
    protected virtual bool SetProperty<T>(
        ref T backingStore,
        T value,
        [CallerMemberName] string propertyName = "",
        Action onChanging = default,
        Action onChanged = default)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        OnPropertyChanging(propertyName);
        onChanging?.Invoke();
        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    #region INotifyPropertyChanging

    /// <summary>
    ///     Raises the event of <paramref name="propertyName" /> is about to change.
    /// </summary>
    /// <param name="propertyName">Name of the property being changed.</param>
    protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = "")
    {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }

    /// <inheritdoc cref="INotifyPropertyChanging.PropertyChanging" />
    public event PropertyChangingEventHandler PropertyChanging;

    #endregion

    #region INotifyPropertyChanged

    /// <summary>
    ///     Raises the event of <paramref name="propertyName" /> having just changed.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged" />
    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region IDisposable

    private bool _disposedValue;

    /// <summary>
    ///     Disposes of managed and unmanaged resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> if currently disposing of objects. Otherwise, <c>false</c>.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // Dispose managed resources.
            }

            _disposedValue = true;
        }
    }

    void IDisposable.Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region IEquatable<string>

    bool IEquatable<string>.Equals(string otherId)
    {
        return Id == otherId;
    }

    /// <summary>
    ///     Determines whether this instance and another specified <see cref="IFeatureFlag" /> object are equal.
    /// </summary>
    /// <param name="other">The specified <see cref="IFeatureFlag" /> object.</param>
    /// <returns><c>true</c> if the specified <see cref="IFeatureFlag" /> object is equal to this.</returns>
    protected virtual bool Equals(IFeatureFlag other)
    {
        return Id == other.Id;
    }

    /// <summary>
    ///     Returns a boolean indicating if the passed in object obj is Equal to this.
    /// </summary>
    /// <param name="obj">The passed in object.</param>
    /// <returns><c>true</c> if the passed in object is equal to this.</returns>
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BaseFeatureFlag)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// Determines whether two specified <see cref="IFeatureFlag" /> objects are equal.
    /// </summary>
    /// <param name="left">The left operand of the equality operator.</param>
    /// <param name="right">The right operand of the equality operator.</param>
    /// <returns><c>true</c> if the two operands are equal. Otherwise, <c>false</c>.</returns>
    public static bool operator ==(BaseFeatureFlag left, BaseFeatureFlag right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two specified <see cref="IFeatureFlag" /> objects are unequal to one another.
    /// </summary>
    /// <param name="left">The left operand of the inequality operator.</param>
    /// <param name="right">The right operand of the inequality operator.</param>
    /// <returns><c>true</c> if the two operands are unequal. Otherwise, <c>false</c>.</returns>
    public static bool operator !=(BaseFeatureFlag left, BaseFeatureFlag right)
    {
        return !Equals(left, right);
    }

    #endregion
}
