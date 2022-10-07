Shader "Custom/双色Shader"
{
    //属性
    Properties
    {
        _MainTex("MainTex(RGBA)", 2D) = "white" {} //材质1
        _GrayTex("GrayTex(RGBA)",2D) = "white" {} //材质2
        _CurrentPos("CurrentPos",Range(0,1)) = 1 //当前位置
    }

        SubShader
        {
            LOD 100
            Cull Off Lighting Off ZWrite Off ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha

            Tags
            {
                "Queue" = "Overlay"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
            }

            Pass{
            CGPROGRAM
            #pragma vertex vert  //定义顶点着色器处理单元
            #pragma fragment frag  //定义片段着色器处理单元

            #include "UnityCG.cginc"  
            //变量定义
            uniform sampler2D _MainTex;
            uniform sampler2D _GrayTex;
            uniform fixed  _CurrentPos;
            uniform fixed4 _MainTex_ST;

            //顶点输入结构
            struct vertexInput {
                fixed4 vertex : POSITION;
                fixed4 texcoord0 : TEXCOORD0;
            };

            //片段输入结构（顶点输出）
            struct fragmentInput {
                fixed4 position : SV_POSITION;
                fixed4 texcoord0 : TEXCOORD0;
                fixed2 uv : _MainTex_ST;
            };

            //顶点着色器处理单元
            fragmentInput vert(vertexInput i) {
                fragmentInput o;
                o.position = UnityObjectToClipPos(i.vertex); //转换物体空间到相机空间
                o.texcoord0 = i.texcoord0;
                o.uv = TRANSFORM_TEX(i.texcoord0, _MainTex) - float2(_Time.y / 10, 0);; //应用材质的Tilling和offset属性

                return o;
            }

            //片段着色器处理单元
            fixed4 frag(fragmentInput i) : SV_Target{
                //判断
                if (i.texcoord0.y >= _CurrentPos)
                    return tex2D(_GrayTex, i.uv);
                else
                    return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
        }
}