using System;
public readonly struct Either<L, R>
{
    private readonly R _right;
    private readonly L _left;
    private readonly bool _isRight;
    private readonly bool _isLeft;

    private Either(R right)
    {
        this._isRight = true;
        this._isLeft = false;
        this._right = right;
        this._left = default;
    }
    private Either(L left)
    {
        this._isLeft = true;
        this._isRight = false;
        this._left = left;
        this._right = default;
    }

    public static implicit operator Either<L, R>(R value) =>
        new Either<L, R>(value);

    public static implicit operator Either<L, R>(L value) =>
        new Either<L, R>(value);

    public Either<L, U> Map<U>(Func<R, U> map) =>
        this._isRight
            ? new Either<L, U>(map(_right))
            : new Either<L, U>(_left);

    public Either<L, U> Bind<U>(Func<R, Either<L, U>> f) =>
        this._isRight
            ? f(_right)
            : new Either<L, U>(_left);
    
    public T Switch<T>(Func<L, T> leftFunc, Func<R, T> rightFunc) =>
        this._isRight
            ? rightFunc(_right)
            : leftFunc(_left);
}

public static class EitherExtension
{
    public static Either<L, V> SelectMany<U, V, L, R>(this Either<L,R> first, Func<R, Either<L, U>> getSecond, Func<R, U, V> project)
    {
        return first.Bind(a => getSecond(a).Map(b => project(a, b)));
    }
    public static Either<L, U> Select<U,L,R>(this Either<L,R> first, Func<R, U> map) => first.Map(map);
}