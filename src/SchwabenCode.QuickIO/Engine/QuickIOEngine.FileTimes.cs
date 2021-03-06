﻿// <copyright company="Benjamin Abt ( http://www.benjamin-abt.com - http://quickIO.NET )">
//      Copyright (c) 2016 Benjamin Abt Rights Reserved - DO NOT REMOVE OR EDIT COPYRIGHT
// </copyright>
// <author>Benjamin Abt</author>

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using SchwabenCode.QuickIO.Win32;

namespace SchwabenCode.QuickIO.Engine
{

    internal static partial class QuickIOEngine
    {
        /// <summary>
        /// Sets the dates and times of given directory or file.
        /// </summary>
        /// <param name="pathInfo">Affected file or directory</param>     
        /// <param name="creationTimeUtc">The time that is to be used (UTC)</param>
        /// <param name="lastAccessTimeUtc">The time that is to be used (UTC)</param>
        /// <param name="lastWriteTimeUtc">The time that is to be used (UTC)</param>
        public static void SetAllFileTimes( string pathUnc, DateTime creationTimeUtc, DateTime lastAccessTimeUtc, DateTime lastWriteTimeUtc )
        {
            long longCreateTime = creationTimeUtc.ToFileTime();
            long longAccessTime = lastAccessTimeUtc.ToFileTime();
            long longWriteTime = lastWriteTimeUtc.ToFileTime();

            using( SafeFileHandle fileHandle = OpenReadWriteFileSystemEntryHandle(pathUnc) )
            {
                if( Win32SafeNativeMethods.SetAllFileTimes( fileHandle, ref longCreateTime, ref longAccessTime, ref longWriteTime ) == 0 )
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    Win32ErrorCodes.NativeExceptionMapping(pathUnc, win32Error );
                }
            }
        }

        /// <summary>
        /// Sets the time at which the file or directory was created (UTC)
        /// </summary>
        /// <param name="pathUnc">Affected file or directory</param>     
        /// <param name="utcTime">The time that is to be used (UTC)</param>
        public static void SetCreationTimeUtc( string pathUnc, DateTime utcTime )
        {
            long longTime = utcTime.ToFileTime();
            using( SafeFileHandle fileHandle = OpenReadWriteFileSystemEntryHandle(pathUnc) )
            {
                if( !Win32SafeNativeMethods.SetCreationFileTime( fileHandle, ref longTime, IntPtr.Zero, IntPtr.Zero ) )
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    Win32ErrorCodes.NativeExceptionMapping(pathUnc, win32Error );
                }
            }
        }

        /// <summary>
        /// Sets the time at which the file or directory was last written to (UTC)
        /// </summary>
        /// <param name="pathUnc">Affected file or directory</param>     
        /// <param name="utcTime">The time that is to be used (UTC)</param>
        public static void SetLastWriteTimeUtc( string pathUnc, DateTime utcTime )
        {
            Contract.Requires(pathUnc != null );

            long longTime = utcTime.ToFileTime();
            using( SafeFileHandle fileHandle = OpenReadWriteFileSystemEntryHandle(pathUnc) )
            {
                if( !Win32SafeNativeMethods.SetLastWriteFileTime( fileHandle, IntPtr.Zero, IntPtr.Zero, ref longTime ) )
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    Win32ErrorCodes.NativeExceptionMapping(pathUnc, win32Error );
                }
            }
        }

        /// <summary>
        /// Sets the time at which the file or directory was last accessed to (UTC)
        /// </summary>
        /// <param name="pathUnc">Affected file or directory</param>     
        /// <param name="utcTime">The time that is to be used (UTC)</param>
        public static void SetLastAccessTimeUtc( string pathUnc, DateTime utcTime )
        {
            Contract.Requires(pathUnc != null );

            long longTime = utcTime.ToFileTime();
            using( SafeFileHandle fileHandle = OpenReadWriteFileSystemEntryHandle(pathUnc) )
            {
                if( !Win32SafeNativeMethods.SetLastAccessFileTime( fileHandle, IntPtr.Zero, ref longTime, IntPtr.Zero ) )
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    Win32ErrorCodes.NativeExceptionMapping(pathUnc, win32Error );
                }
            }
        }

    }
}