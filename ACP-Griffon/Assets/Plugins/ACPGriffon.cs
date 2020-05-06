﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace com.adobe.marketing.mobile
{
    public class ACPGriffon
    {
        /* ===================================================================
        * Static Helper objects for our JNI access
        * =================================================================== */
        #if UNITY_ANDROID

        static AndroidJavaClass griffon =
            new AndroidJavaClass("com.adobe.marketing.mobile.Griffon");

        #endif

        /* ===================================================================
        * extern declarations for iOS Methods
        * =================================================================== */
        #if UNITY_IPHONE

            [DllImport ("__Internal")]
            private static extern bool Griffon_RegisterExtension();

            [DllImport ("__Internal")]
            private static extern void Griffon_StartSession(string url);

            [DllImport ("__Internal")]
            private static extern System.IntPtr Griffon_ExtensionVersion();
            
        #endif

        /*--------------------------------------------------------
        * Methods
        *----------------------------------------------------------------------*/

        public static bool GriffonRegisterExtension()
        {
            #if UNITY_IPHONE && !UNITY_EDITOR
                return Griffon_RegisterExtension();

            #elif UNITY_ANDROID && !UNITY_EDITOR
                if(AndroidJNI.AttachCurrentThread() >= 0){
                    return griffon.CallStatic<bool> ("registerExtension");
                    
                }
                return false;
            #else
                return false;
            #endif
        }

        public static void StartSession(string url)
        {

            #if UNITY_IPHONE && !UNITY_EDITOR
                Griffon_StartSession(url);
            #elif UNITY_ANDROID && !UNITY_EDITOR
                if(AndroidJNI.AttachCurrentThread() >= 0){
                    griffon.CallStatic("startSession", url);    
                }
            #endif
        }

        public static string GriffonExtensionVersion()
        {
            
            #if UNITY_IPHONE && !UNITY_EDITOR
                return Marshal.PtrToStringAnsi(Griffon_ExtensionVersion());
            #elif UNITY_ANDROID && !UNITY_EDITOR
                if(AndroidJNI.AttachCurrentThread() >= 0){
                    return griffon.CallStatic<string> ("extensionVersion");   
                }
                return "";
            #else
                return "";
            #endif
        }                        
    }
}