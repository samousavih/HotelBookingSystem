using System;
using System.Threading.Tasks;

namespace HotelBookingSystem.Core;
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

    public static Either<L, R> From(R value) => new Either<L, R>(value);

    public Either<L, R2> Map<R2>(Func<R, R2> map) =>
        this._isRight
            ? new Either<L, R2>(map(_right))
            : new Either<L, R2>(_left);

    public Either<L, R2> Bind<R2>(Func<R, Either<L, R2>> f) =>
        this._isRight
            ? f(_right)
            : new Either<L, R2>(_left);

    public T Switch<T>(Func<L, T> leftFunc, Func<R, T> rightFunc) =>
        this._isRight
            ? rightFunc(_right)
            : leftFunc(_left);

    public async Task<Either<L, R2>> BindAsync<R2>(Func<R, Task<Either<L, R2>>> bind) =>
        this._isRight
            ? await bind(this._right).ConfigureAwait(false)
            : new Either<L, R2>(_left);

}

public static class EitherExtension
{
    public static Either<L, V> SelectMany<U, V, L, R>(this Either<L,R> first, Func<R, Either<L, U>> getSecond, Func<R, U, V> project)
    {
        return first.Bind(a => getSecond(a).Map(b => project(a, b)));
    }
    public static Either<L, U> Select<U,L,R>(this Either<L,R> first, Func<R, U> map) => first.Map(map);

}

public static class TaskExtension 
{
    public static Task<Either<L,R>> ToEitherAsync<L,R>(this Either<L,R> either)
    {
        return Task.FromResult(either);
    }
    
    public static Task<Either<L,R>> ToEitherAsync<L,R>(this R value)
    {
        return Task.FromResult(Either<L,R>.From(value));
    }

    public static async Task<Either<L, U>> Select<U,L,R>(this Task<Either<L,R>> first, Func<R, U> map) => (await first).Map(map);
    public static async Task<Either<L, V>> SelectMany<U, V, L, R>(this Task<Either<L,R>> first, Func<R, Task<Either<L, U>>> getSecond, Func<R, U, V> project)
    {
        return await (await first).BindAsync(async a => (await getSecond(a)).Map(b => project(a, b)));
    }
    
    public static async Task<Either<L, V>> SelectMany<U, V, L, R>(this Either<L,R> first, Func<R, Task<Either<L, U>>> getSecond, Func<R, U, V> project)
    {
        return await first.BindAsync(async a => (await getSecond(a)).Map(b => project(a, b)));
    }
    
    public static async Task<Either<L, V>> SelectMany<U, V, L, R>(this Task<Either<L,R>> first, Func<R, Either<L, U>> getSecond, Func<R, U, V> project)
    {
        return (await first).Bind(a => getSecond(a).Map(b => project(a, b)));
    }
}