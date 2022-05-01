#version 400 core


in vec2 pass_textureCoords;
in vec3 surfaceNormal;
in vec3 toLightVector;
in vec3 toCameraVector;

out vec4 out_Color;

uniform sampler2D textureSampler;
uniform vec3 lightColour;
uniform float shineDamper;
uniform float reflectivity;
uniform vec4 render_color;

void main(void){

vec3 unitNormal  = normalize(surfaceNormal);
vec3 unitLight =  normalize(toLightVector);
float nDot1 = dot(unitNormal,unitLight);
float brightness = max(nDot1,0.5);
vec3 diffuse = brightness * lightColour;
vec3 unitVectorToCamera = normalize(toCameraVector);

vec3 lightDirection = -unitLight;
vec3 reflectedLightDirection = reflect(lightDirection,unitNormal);

float specularFactor = dot(reflectedLightDirection,unitVectorToCamera);

specularFactor = max(specularFactor,0.0);

float dempedFactor = pow(specularFactor,shineDamper);
vec3 finalSpecular = dempedFactor * reflectivity * lightColour;

out_Color =  render_color * (vec4(diffuse,1.0) * texture(textureSampler,pass_textureCoords) + vec4(finalSpecular,1.0));

}