#include<bits/stdc++.h>
using namespace std;
#define Max 100009
struct ff
{
    long long int f;
    long long int v;
    long long int u;
}t[4*Max];
void bu(long long int z,long long int a[],long long int l,long long int r)
{
    if(l>r)
        return;
    if(l==r)
    {
        t[z].u=0;
        t[z].v=0;
        if(a[l]%3==0)
            t[z].f=1;
        else
            t[z].f=0;
        return;
    }
    long long int m=(r+l)/2;
    bu(2*z+1,a,l,m);
    bu(2*z+2,a,m+1,r);
    t[z].f=t[2*z+1].f+t[2*z+2].f;
    return;
}
void up(long long int z,long long int a[],long long int l,long long int r,long long int x,long long int y)
{
    if(l>r)
    {
        return;
    }
    if(t[z].u!=0)
    {
        t[z].v+=t[z].u;
        if(l!=r)
        {
            t[2*z+1].u+=t[z].u;
            t[2*z+2].u+=t[z].u;
        }
        if(l==r)
        {
            if((t[z].v)%3==0)
                t[z].f=1;
            else
                t[z].f=0;
        }
        t[z].u=0;
    }
    if(x>r || y<l)
    {
        if(t[z].v%3==0)
            t[z].f=1;
        else
            t[z].f=0;
        return;
    }
    if(x<=l && y>=r)
    {
        t[z].v+=1;
        if(l!=r)
        {
            t[2*z+1].u+=1;
            t[2*z+2].u+=1;
        }
        if(l==r)
        {
            if((t[z].v)%3==0)
                t[z].f=1;
            else
                t[z].f=0;
        }
        return;
    }
    long long int m=(r+l)/2;
    up(2*z+1,a,l,m,x,y);
    up(2*z+2,a,m+1,r,x,y);
    t[z].f=t[2*z+1].f+t[2*z+2].f;
    return;
}
ff qu(long long int z,long long int a[],long long int l,long long int r,long long int x,long long int y)
{
    if(l>r)
    {
        ff t1;
        t1.f=0;
        return t1;
    }
    if(t[z].u!=0)
    {
        t[z].v+=t[z].u;
        if(l!=r)
        {
            t[2*z+1].u+=t[z].u;
            t[2*z+2].u+=t[z].u;
        }
        t[z].u=0;
    }
    if(x>r || y<l)
    {
        ff t1;
        t1.f=0;
        return t1;
    }
    if(x<=l && y>=r)
    {
        return t[z];
    }
    long long int m=(r+l)/2;
    ff t1;
    ff t2=qu(2*z+1,a,l,m,x,y);
    ff t3=qu(2*z+2,a,m+1,r,x,y);
    t1.f=t2.f+t3.f;
    return t1;
}
int main()
{
    long long int n,q,i,a1,b,c;
    cin>>n>>q;
    long long int a[n]={0};
    ff x;
    bu(0,a,0,n-1);
    while(q--)
    {
        cin>>a1>>b>>c;
        if(a1==1)
        {
            x=qu(0,a,0,n-1,b,c);
            for(i=0;i<7;i++)
                cout<<t[i].v<<" "<<t[i].f<<" "<<t[i].u<<"\n";
            cout<<x.f<<"\n";
        }
        else if(a1==0)
        {
            up(0,a,0,n-1,b,c);
            for(i=0;i<7;i++)
                cout<<t[i].v<<" "<<t[i].f<<" "<<t[i].u<<"\n";
        }
    }
}
