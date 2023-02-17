using System;
using System.Collections;
using System.Collections.Generic;
using HotelBookingSystem.BookingCreation;

public readonly struct Either<L,R> : IEnumerable<EitherData<L,R>>
{
    public readonly static Either<L, R> Bottom = new Either<L, R>();

    internal readonly R right;
    internal readonly L left;
    private readonly bool isRight;
    private readonly bool isLeft;

    private Either(R right)
    {
        this.isRight = true;
        this.isLeft = false;
        this.right = right;
        this.left = default(L);
    }
    private Either(L left)
    {
        this.isLeft = true;
        this.isRight = false;
        this.left = left;
        this.right = default(R);
    }

    public static implicit operator Either<L, R>(R value) =>
        new Either<L, R>(value);
    
    public static implicit operator Either<L, R>(L value) =>
        new Either<L, R>(value);



    public IEnumerator<EitherData<L, R>> GetEnumerator()
    {
        yield return new EitherData<L, R>(right, left);
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Either<L, U> Select<U>(Func<R, U> map)
    {
        return this switch
        {
            {isRight:true} => new Either<L, U>(map(right)),
            {isLeft:true} => new Either<L, U>(left),
        };
    }
    
    public Either<L, V> SelectMany<U, V>(Func<R, Either<L, U>> bind, Func<R, U, V> project)
    {
        return this.Bind(a =>
            bind(a).Select(b =>
                project(a, b)));
    }
    
    public Either<L,U> Bind<U>(Func<R, Either<L,U>> f) =>  
    this switch
    {
        {isRight:true} => f(right),
        {isLeft:true} => new Either<L, U>(left),
    };
    public static Either<L, R> From(R value)
    {
        return new Either<L, R>(value);
    }
}


public class EitherData<L, R> 
{
    public readonly R Right;
    public readonly L Left;

    public EitherData(R right, L left)
    {
        Right = right;
        Left = left;
    }
}