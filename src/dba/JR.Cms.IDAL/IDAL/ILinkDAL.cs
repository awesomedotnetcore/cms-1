//
//
//  Generated by StarUML(tm) C# Add-In
//
//  @ Project : OSite
//  @ File Name : ILinkDAL.cs
//  @ Date : 2011/8/23
//  @ Author : 
//
//

namespace Spc.IDAL
{
    using J6.Data;
    using Spc.Models;

    public interface ILinkDAL
    {
        int Add(Link link);
        int Update(Link link);
        int Delete(int id);
        int SetVisible(int id, bool visible);
        void GetAll(DataReaderFunc func);
    }
}
