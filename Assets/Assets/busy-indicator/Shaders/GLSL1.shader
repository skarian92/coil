Shader "Custom/GLSL1" {
	Properties
	{
        _AmbientLightColor ("Ambient Light Color", Color) = (1,1,1,1)
	    u_resolution ("Resolution", Vector) = (1200.0, 800.0, 0, 0)
	    u_mouse ("Mouse", Vector) = (1200.0, 800.0, 0, 0)
	    u_time ("Time", Float) = 0.05

	}
   SubShader { // Unity chooses the subshader that fits the GPU best
      Pass { // some shaders require multiple passes
         GLSLPROGRAM // here begins the part in Unity's GLSL

         #ifdef VERTEX // here begins the vertex shader

         void main() // all vertex shaders define a main() function
         {
            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
               // this line transforms the predefined attribute 
               // gl_Vertex of type vec4 with the predefined
               // uniform gl_ModelViewProjectionMatrix of type mat4
               // and stores the result in the predefined output 
               // variable gl_Position of type vec4.
         }

         #endif // here ends the definition of the vertex shader


         #ifdef FRAGMENT // here begins the fragment shader

		#ifdef GL_ES
		precision mediump float;
		#endif

		uniform vec2 u_resolution;
		uniform vec2 u_mouse;
		uniform float u_time;

		// Plot a line on Y using a value between 0.0-1.0
		float plot(vec2 st, float pct){
		  return  smoothstep( pct-0.02, pct, st.y) - 
		          smoothstep( pct, pct+0.02, st.y);
		}

		void main() {
//			vec2 st = gl_FragCoord.xy/u_resolution;
//
//		    float y = st.x;
//
//		    vec3 color = vec3(y);
//		    
//		    // Plot a line
//		    float pct = plot(st,y);
//		    color = (1.0-pct)*color+pct*vec3(0.0,1.0,0.0);
//		    
//			gl_FragColor = vec4(color,1.0);

			vec3 c;
			float l,z=u_time;
			for(int i=0;i<3;i++) {
				vec2 uv,p=gl_FragCoord.xy/u_resolution;
				uv=p;
				p-=.5;
				p.x*=u_resolution.x/u_resolution.y;
				z+=.07;
				l=length(p);
				uv+=p/l*(sin(z)+1.)*abs(sin(l*9.-z*2.));
				// org: 0.1
				c[i]=.005/length(abs(mod(uv,1.)-.5));
			}
			gl_FragColor=vec4(c/l,u_time/10);
		}

         #endif // here ends the definition of the fragment shader

         ENDGLSL // here ends the part in GLSL 
      }
   }
}
