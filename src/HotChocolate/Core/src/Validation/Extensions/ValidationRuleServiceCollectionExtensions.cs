using HotChocolate.Validation.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.Validation
{
    public static class ValidationRuleServiceCollectionExtensions
    {
        /// <summary>
        /// Every argument provided to a field or directive must be defined
        /// in the set of possible arguments of that field or directive.
        ///
        /// http://facebook.github.io/graphql/June2018/#sec-Argument-Names
        ///
        /// AND
        ///
        /// Fields and directives treat arguments as a mapping of argument name
        /// to value.
        ///
        /// More than one argument with the same name in an argument set
        /// is ambiguous and invalid.
        ///
        /// http://facebook.github.io/graphql/June2018/#sec-Argument-Uniqueness
        ///
        /// AND
        ///
        /// Arguments can be required. An argument is required if the argument
        /// type is non‐null and does not have a default value. Otherwise,
        /// the argument is optional.
        ///
        /// http://facebook.github.io/graphql/June2018/#sec-Required-Arguments
        /// </summary>
        public static IServiceCollection AddArgumentsAreValidRule(
            this IServiceCollection services)
        {
            return services.AddValidationRule<ArgumentsVisitor>();
        }

        /// <summary>
        /// GraphQL servers define what directives they support.
        /// For each usage of a directive, the directive must be available
        /// on that server.
        ///
        /// http://spec.graphql.org/June2018/#sec-Directives-Are-Defined
        ///
        /// AND
        ///
        /// GraphQL servers define what directives they support and where they
        /// support them.
        ///
        /// For each usage of a directive, the directive must be used in a
        /// location that the server has declared support for.
        ///
        /// http://spec.graphql.org/June2018/#sec-Directives-Are-In-Valid-Locations
        ///
        /// AND
        ///
        /// Directives are used to describe some metadata or behavioral change on
        /// the definition they apply to.
        ///
        /// When more than one directive of the
        /// same name is used, the expected metadata or behavior becomes ambiguous,
        /// therefore only one of each directive is allowed per location.
        ///
        /// http://spec.graphql.org/draft/#sec-Directives-Are-Unique-Per-Location
        /// </summary>
        public static IServiceCollection AddDirectiveRules(
            this IServiceCollection services)
        {
            return services.AddValidationRule<DirectivesVisitor>();
        }

        /// <summary>
        /// GraphQL execution will only consider the executable definitions
        /// Operation and Fragment.
        ///
        /// Type system definitions and extensions are not executable,
        /// and are not considered during execution.
        ///
        /// To avoid ambiguity, a document containing TypeSystemDefinition
        /// is invalid for execution.
        ///
        /// GraphQL documents not intended to be directly executed may
        /// include TypeSystemDefinition.
        ///
        /// http://spec.graphql.org/June2018/#sec-Executable-Definitions
        /// </summary>
        public static IServiceCollection AddExecutableDefinitionRule(
            this IServiceCollection services)
        {
            return services.AddSingleton<IDocumentValidatorRule, ExecutableDefinitionsRule>();
        }

        /// <summary>
        /// The target field of a field selection must be defined on the scoped
        /// type of the selection set. There are no limitations on alias names.
        ///
        /// http://spec.graphql.org/June2018/#sec-Field-Selections-on-Objects-Interfaces-and-Unions-Types
        ///
        /// AND
        ///
        /// Field selections on scalars or enums are never allowed,
        /// because they are the leaf nodes of any GraphQL query.
        ///
        /// Conversely the leaf field selections of GraphQL queries
        /// must be of type scalar or enum. Leaf selections on objects,
        /// interfaces, and unions without subfields are disallowed.
        ///
        /// http://spec.graphql.org/June2018/#sec-Leaf-Field-Selections
        /// </summary>
        public static IServiceCollection AddFieldRules(
            this IServiceCollection services)
        {
            return services.AddValidationRule<FieldsVisitor>();
        }

        /// <summary>
        /// Fragment definitions are referenced in fragment spreads by name.
        /// To avoid ambiguity, each fragment’s name must be unique within a
        /// document.
        ///
        /// http://spec.graphql.org/June2018/#sec-Fragment-Name-Uniqueness
        ///
        /// AND
        ///
        /// Defined fragments must be used within a document.
        ///
        /// http://spec.graphql.org/June2018/#sec-Fragments-Must-Be-Used
        ///
        /// AND
        ///
        /// Fragments can only be declared on unions, interfaces, and objects.
        /// They are invalid on scalars.
        /// They can only be applied on non‐leaf fields.
        /// This rule applies to both inline and named fragments.
        ///
        /// http://spec.graphql.org/June2018/#sec-Fragments-On-Composite-Types
        ///
        /// AND
        ///
        /// Fragments are declared on a type and will only apply when the
        /// runtime object type matches the type condition.
        ///
        /// They also are spread within the context of a parent type.
        ///
        /// A fragment spread is only valid if its type condition could ever
        /// apply within the parent type.
        ///
        /// http://spec.graphql.org/June2018/#sec-Fragment-spread-is-possible
        ///
        /// AND
        ///
        /// Named fragment spreads must refer to fragments defined within the
        /// document.
        ///
        /// It is a validation error if the target of a spread is not defined.
        ///
        /// http://spec.graphql.org/June2018/#sec-Fragment-spread-target-defined
        ///
        /// AND
        ///
        /// The graph of fragment spreads must not form any cycles including
        /// spreading itself. Otherwise an operation could infinitely spread or
        /// infinitely execute on cycles in the underlying data.
        ///
        /// http://spec.graphql.org/June2018/#sec-Fragment-spreads-must-not-form-cycles
        ///
        /// AND
        ///
        /// Fragments must be specified on types that exist in the schema.
        /// This applies for both named and inline fragments.
        /// If they are not defined in the schema, the query does not validate.
        ///
        /// http://spec.graphql.org/June2018/#sec-Fragment-Spread-Type-Existence
        /// </summary>
        public static IServiceCollection AddFragmentRules(
            this IServiceCollection services)
        {
            return services.AddValidationRule<FragmentsVisitor>();
        }

        /// <summary>
        /// Every input field provided in an input object value must be defined in
        /// the set of possible fields of that input object’s expected type.
        ///
        /// http://spec.graphql.org/June2018/#sec-Input-Object-Field-Names
        ///
        /// AND
        ///
        /// Input objects must not contain more than one field of the same name,
        /// otherwise an ambiguity would exist which includes an ignored portion
        /// of syntax.
        ///
        /// http://spec.graphql.org/June2018/#sec-Input-Object-Field-Uniqueness
        ///
        /// AND
        ///
        /// Input object fields may be required. Much like a field may have
        /// required arguments, an input object may have required fields.
        ///
        /// An input field is required if it has a non‐null type and does not have
        /// a default value. Otherwise, the input object field is optional.
        ///
        /// http://spec.graphql.org/June2018/#sec-Input-Object-Required-Fields
        /// </summary>
        public static IServiceCollection AddInputObjectRules(
            this IServiceCollection services)
        {
            return services.AddValidationRule<InputObjectVisitor>();
        }

        /// <summary>
        /// If any operation defines more than one variable with the same name,
        /// it is ambiguous and invalid. It is invalid even if the type of the
        /// duplicate variable is the same.
        ///
        /// http://spec.graphql.org/June2018/#sec-Validation.Variables
        ///
        /// AND
        ///
        /// Variables can only be input types. Objects,
        /// unions, and interfaces cannot be used as inputs.
        ///
        /// http://spec.graphql.org/June2018/#sec-Variables-Are-Input-Types
        ///
        /// AND
        ///
        /// All variables defined by an operation must be used in that operation
        /// or a fragment transitively included by that operation.
        ///
        /// Unused variables cause a validation error.
        ///
        /// http://spec.graphql.org/June2018/#sec-All-Variables-Used
        ///
        /// AND
        ///
        /// Variables are scoped on a per‐operation basis. That means that
        /// any variable used within the context of an operation must be defined
        /// at the top level of that operation
        ///
        /// http://spec.graphql.org/June2018/#sec-All-Variable-Uses-Defined
        ///
        /// AND
        ///
        /// Variable usages must be compatible with the arguments
        /// they are passed to.
        ///
        /// Validation failures occur when variables are used in the context
        /// of types that are complete mismatches, or if a nullable type in a
        ///  variable is passed to a non‐null argument type.
        ///
        /// http://spec.graphql.org/June2018/#sec-All-Variable-Usages-are-Allowed
        /// </summary>
        public static IServiceCollection AddVariableRules(
            this IServiceCollection services)
        {
            return services.AddValidationRule<VariablesVisitor>();
        }

        /// <summary>
        /// GraphQL allows a short‐hand form for defining query operations
        /// when only that one operation exists in the document.
        ///
        /// http://spec.graphql.org/June2018/#sec-Lone-Anonymous-Operation
        ///
        /// AND
        ///
        /// Each named operation definition must be unique within a document
        /// when referred to by its name.
        ///
        /// http://spec.graphql.org/June2018/#sec-Operation-Name-Uniqueness
        /// </summary>
        public static IServiceCollection AddOperationRules(
            this IServiceCollection services)
        {
            return services.AddSingleton<IDocumentValidatorRule, OperationRule>();
        }

        public static IServiceCollection AddValidationRule<T>(
            this IServiceCollection services)
            where T : DocumentValidatorVisitor, new()
        {
            return services.AddSingleton<IDocumentValidatorRule, DocumentValidatorRule<T>>();
        }
    }
}
